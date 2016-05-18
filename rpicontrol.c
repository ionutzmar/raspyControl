#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <wiringPi.h>

int sockfd, newsockfd;
struct sockaddr_in serv_addr, cli_addr;
const int portno = 8887;
int pins[] = {8, 9, 7, 0, 2, 3, 12, 13, 14, 21, 22, 23, 24, 25, 15,  16, 1, 4, 5, 6, 10, 11, 26, 27, 28, 29};

void resetPins()
{
    int i;
    for(i = 0; i < 26; i++)
    {
	pinMode(pins[i], 0);
	digitalWrite(pins[i], 0);
    }

}
void error(const char *msg)
{
    perror(msg);
    exit(1);
}

void createSocket()
{
    sockfd = socket(AF_INET, SOCK_STREAM, 0);
    if (sockfd < 0)
    error("ERROR opening socket");
    bzero((char *) &serv_addr, sizeof(serv_addr));
    serv_addr.sin_family = AF_INET;
    serv_addr.sin_addr.s_addr = INADDR_ANY;
    serv_addr.sin_port = htons(portno);
    if (bind(sockfd, (struct sockaddr *) &serv_addr,
        sizeof(serv_addr)) < 0)
        error("ERROR on binding");
    listen(sockfd,5);
    printf("Waiting for the client connect...\n");

}

void acceptClient()
{
    socklen_t clilen = sizeof(cli_addr);
    newsockfd = accept(sockfd,
                (struct sockaddr *) &cli_addr,
                &clilen);
    if (newsockfd < 0)
         error("ERROR on accept");
    printf("Client connected! :)\n");

}
int main(int argc, char *argv[])
{
    wiringPiSetup();
    createSocket();
reconnect:
    acceptClient();
    resetPins();
    char buffer[4];
    char bufferOut[4];
    bzero(buffer,4);
    while(read(newsockfd,  buffer, 4) > 0)
    {
        //if(buffer[3] != 100)
        //{
        //    printf("Warning! Someone else is trying to control your raspberry!!\n");
        //    close(newsockfd);
        //    close(sockfd);
        //    return 0;
        //}
	pinMode(buffer[0], buffer[1]);
        if(buffer[1])
            digitalWrite(buffer[0], buffer[2]);
	else
	{
	    bzero(bufferOut, 4);
	    bufferOut[0] = buffer[0];
	    bufferOut[2] = digitalRead(buffer[0]);
	    bufferOut[3] = 102;
	    write(newsockfd, bufferOut, 4);
	}
        int i;
        for(i = 0; i < 4 ; i++)
        {
	    //digitalWrite(pins[i], buffer[i]);
	    printf("%d, ", buffer[i]);
	}
	printf("\n");
    }
    printf("Client disconnected! Waiting for a new one...\n");
    close(newsockfd);
goto reconnect;
    close(sockfd);
    return 0;
}
