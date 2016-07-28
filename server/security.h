#ifndef SECURITY_H
#define SECURITY_H

char* sha512(const char* toHash);
void setuser(char* user);
void removeuser(void);
int verifyUser(char* user);
int verifyPassword(char* password);

#endif //SECURITY_H
