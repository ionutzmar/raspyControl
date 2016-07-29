#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <signal.h>
#include <wiringPi.h>
#include "security.h"
#include "rsa.h"

int sockfd, clientsockfd;
struct sockaddr_in serv_addr, cli_addr;
const int portno = 8887;
int pins[] = {8, 9, 7, 0, 2, 3, 12, 13, 14, 21, 22, 23, 24, 25, 15,  16, 1, 4, 5, 6, 10, 11, 26, 27, 28, 29};

#define BUFFERSIZE 16
#define SIGNATURE 5678;

void closeFd(int a)  //closes the file descriptors if the ctrl+c is pressed
{
    if(a != 2)
        return;
    close(clientsockfd);
    close(sockfd);
    exit(EXIT_FAILURE);
 }
void resetPins()
{
    int i;
    for(i = 0; i < 26; i++)
    {
        digitalWrite(pins[i], 0);
        pinMode(pins[i], 0);
    }

}
void error(const char *msg)
{
    perror(msg);
    exit(EXIT_FAILURE);
}

void createSocket()
{
    sockfd = socket(AF_INET, SOCK_STREAM, 0);
    if (sockfd < 0)
        error("ERROR opening socket");
    int optval = 1;
    if (setsockopt(sockfd, SOL_SOCKET, SO_REUSEADDR, &optval, sizeof(optval)) < 0)
        error("Could not set the sockfd options");
    bzero((char *) &serv_addr, sizeof(serv_addr));
    serv_addr.sin_family = AF_INET;
    serv_addr.sin_addr.s_addr = INADDR_ANY;
    serv_addr.sin_port = htons(portno);
    if (bind(sockfd, (struct sockaddr *) &serv_addr, sizeof(serv_addr)) < 0)
        error("ERROR on binding");
    listen(sockfd, 5);

    printf("Waiting for the client to connect...\n");

}

void acceptClient()
{
    socklen_t clilen = sizeof(cli_addr);
    clientsockfd = accept(sockfd,
                (struct sockaddr *) &cli_addr,
                &clilen);
    if (clientsockfd < 0)
         error("ERROR on accept");
    printf("Client connected! :)\n");

}

int main(int argc, char *argv[])
{
    wiringPiSetup();

    if(argc > 3)
    {
        fprintf(stderr, "Wrong number of command line arguments! See the README for more info.\n");
        exit(EXIT_FAILURE);
    }
    if(argc == 2)
    {
        if(!strcmp(argv[1], "removeuser"))
            removeuser();
        else
        {
            fprintf(stderr, "%s\n", "Wrong command. See the README for more info.\n");
            exit(EXIT_FAILURE);
        }
    }
    if(argc == 3)
    {
        if(!strcmp(argv[1], "setuser"))
            setuser(argv[2]);
        else
        {
            fprintf(stderr, "%s\n", "Wrong command.");
            exit(EXIT_FAILURE);
        }

    }
    signal(SIGINT, closeFd);
    createSocket();
reconnect:
    acceptClient();
    resetPins();
    rsaInitialise();
    int n, e;
    getPublicKey(&n, &e);
    int bufferIn[BUFFERSIZE];
    int bufferOut[BUFFERSIZE];
    char charBuf[BUFFERSIZE];

    bzero(charBuf, BUFFERSIZE * sizeof(char));
    bzero(bufferIn, BUFFERSIZE * sizeof(int));
    bzero(bufferOut, BUFFERSIZE * sizeof(int));

    bufferOut[0] = SIGNATURE;
    bufferOut[1] = n;
    bufferOut[2] = e;
    bufferOut[3] = SIGNATURE;

    write(clientsockfd, bufferOut, 4 * sizeof(int));

    if(read(clientsockfd,  bufferIn, BUFFERSIZE * sizeof(int)) < 1) // read the username
    {
        printf("Client disconnected! Waiting for a new one...\n");
        close(clientsockfd);
        goto reconnect;
    }
    decryptBufferToChar(bufferIn, BUFFERSIZE, charBuf);
    int sw = verifyUser(charBuf);

    if(read(clientsockfd,  bufferIn, BUFFERSIZE * sizeof(int)) < 1) //read the password
    {
        printf("Client disconnected! Waiting for a new one...\n");
        close(clientsockfd);
        goto reconnect;
    }
    decryptBufferToChar(bufferIn, BUFFERSIZE, charBuf);
    if(!sw || !verifyPassword(charBuf))
    {
        bzero(bufferOut, BUFFERSIZE * sizeof(int));
        bufferOut[0] = SIGNATURE;
        bufferOut[1] = 0;
        bufferOut[2] = 0;
        bufferOut[3] = SIGNATURE;
        write(clientsockfd, bufferOut, 4 * sizeof(int));
        printf("%s\n", "Wrong username or password!");
        goto reconnect;
    }

    bzero(bufferOut, BUFFERSIZE * sizeof(int));
    bufferOut[0] = SIGNATURE;
    bufferOut[1] = 1;
    bufferOut[2] = 1;
    bufferOut[3] = SIGNATURE;
    write(clientsockfd, bufferOut, 4 * sizeof(int));

    while(read(clientsockfd,  bufferIn, BUFFERSIZE * sizeof(int)) > 0)
    {
        decryptBuffer(bufferIn, BUFFERSIZE);
        int j;
        for(j = 0; j < BUFFERSIZE; j++)
            printf("%d \n", bufferIn[j]);
        printf("\n");

        pinMode(bufferIn[0], bufferIn[1]);
        if(bufferIn[1])
            digitalWrite(bufferIn[0], bufferIn[2]);
	    else
	    {
            bzero(bufferOut, BUFFERSIZE * sizeof(int));
            bufferOut[0] = bufferIn[0];
            bufferOut[2] = digitalRead(bufferIn[0]);
            bufferOut[3] = 101;
-           write(clientsockfd, bufferOut, BUFFERSIZE * sizeof(int));
	    }
        int i;
        for(i = 0; i < 4 ; i++)
        {
	           //digitalWrite(pins[i], buffer[i]);
               printf("%d, ", bufferIn[i]);
        }
	printf("\n");
    }
    printf("Client disconnected! Waiting for a new one...\n");
    close(clientsockfd);
goto reconnect;
    close(sockfd);
    return 0;
}
