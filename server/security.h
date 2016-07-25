#ifndef SECURITY_H
#define SECURITY_H

char* sha512(const char* toHash);
void setuser(char* user);
void removeuser(void);

#endif //SECURITY_H
