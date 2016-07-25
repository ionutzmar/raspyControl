#include <stdlib.h>
#include <stdio.h>
#include <string.h>

#define _XOPEN_SOURCE
#include <unistd.h>
#define _GNU_SOURCE
#include <crypt.h>

#define MAXLENGTH 15

#include "security.h"

void error(const char *msg);

char* sha512(const char* toHash)
{
    return crypt(toHash, "$6$");
}

void setuser(char* user)
{
    if(strlen(user) > MAXLENGTH)
    {
        fprintf(stderr, "Please choose a username less than %d characters!", MAXLENGTH);
        exit(EXIT_FAILURE);
    }
    printf("Now type a password less than %d characters: ", MAXLENGTH);
    char* password = getpass("");
    if(password == NULL)
        error("Could not read the password: ");
    if(strlen(password) > MAXLENGTH - 1)
    {
        fprintf(stderr, "%s\n", "Password too long.");
        exit(EXIT_FAILURE);
    }
    char* sw = (char*) malloc(MAXLENGTH * sizeof(char));
    strcpy(sw, password);
    password = getpass("Now retype your password: ");
    if(password == NULL)
        error("Could not read the password: ");
    if(!strcmp(password, sw))
    {
        FILE* file = fopen("users", "w");
        if(file == NULL)
            error("Could not create the users database file: ");

        fprintf(file, "%s\n", user);
        fprintf(file, "%s\n", sha512(password));
        printf("User %s successfully created.\n", user);
        free(sw);
        fclose(file);
        exit(EXIT_SUCCESS);
    }
    else
    {
        fprintf(stderr, "%s\n", "Passwords do not match.");
        free(sw);
        exit(EXIT_FAILURE);
    }

}

void removeuser(void)
{
    FILE* file = fopen("users", "w");
    if(file == NULL)
        error("Could not delete the users database file: ");
    fclose(file);
    printf("The user is deleted!\n");
    exit(EXIT_SUCCESS);
}
