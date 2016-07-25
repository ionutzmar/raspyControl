/*
    Author: ionutzmar@gmail.com

    To encrypt something with a public key, use encrypt(int toEncrypt, long long n, long long e);

    To generate a private key, use rsaInitialise(). Then to get your public key call void getPublicKey(int*, int*).
    To decrypt something with your private key, use decrypt(int toDecrypt)
*/

#ifndef RSA_H
#define RSA_H

void rsaInitialise();

void getPublicKey(int*, int*); //the pointers are the pair (n, e); You have to run rsaInitialise() before using it.

int encrypt(int toEncrypt, long long n, long long e);

int decrypt(int toDecrypt); // You have to run rsaInitialise() before using it.

#endif //RSA_H
