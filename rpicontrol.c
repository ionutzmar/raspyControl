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
int pins[] = {0, 1, 2, 3, 4, 5, 6, 21, 22,
    23, 24, 25, 26, 27, 28, 29};

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
    printf("Waitingg for the client connect...\n");

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
    acceptClient();

    char buffer[17];
    bzero(buffer,17);
    while(read(newsockfd,buffer,40) >= 0)
    {
        if(buffer[16] != 597138)
        {
            printf("Warning! Someone else is trying to control your raspberry!!\n", );
            close(newsockfd);
            close(sockfd);
            return 0;
        }
        int i;
        for(i = 0; i < 16; i++)
            digitalWrite(pins[i], buffer[i]);
    }
    error("ERROR reading from socket");
    close(newsockfd);
    close(sockfd);
    return 0;
}