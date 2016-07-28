/*
    This algorithm assumes that there are at least 2 prime numbers in primeNumbers.prime separated by space.

*/

#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <ctype.h>

#include "rsa.h"

typedef long long ll;

static ll p, q, n, m, e, d;

static void error(const char *msg)
{
    perror(msg);
    exit(EXIT_FAILURE);
}

static void readPrimes()  //This function reads 2 arbitrary numbers from primeNumbers.prime. It assumes they are prime.
{
    time_t t;
    srand((unsigned) time(&t));

    FILE* file = fopen("primeNumbers.prime", "r");
    if(file == NULL)
        error("Could not open the file: ");

    if (fseek(file, 0, SEEK_END) < 0)
        error("Could not fseek: ");

    long fileBytes = ftell(file);
    if (fileBytes < 0)
        error("Could not measure the file length: ");

    int i;
    for(i = 0; i < 2; i++)
    {
        long randomByte = rand() % fileBytes;

        if (fseek(file, randomByte, SEEK_SET) < 0)
            error("Could not fseek: ");

        char counter = 0;
        int lastValue = (isdigit(fgetc(file)) > 0) ? 1 : 0;
        char upTo = (lastValue) ? 1 : 2;

        while(randomByte > 0 && counter < upTo)
        {
            fseek(file, --randomByte, SEEK_SET);
            int value = (isdigit(fgetc(file)) > 0) ? 1 : 0;

            if(value != lastValue)
                counter++;
            lastValue = value;
        }

        fseek(file, randomByte, SEEK_SET);
        if(i == 0)
            fscanf(file, "%lli", &p);
        else
            fscanf(file, "%lli", &q);
        if(i == 1 && p == q)
            i--;
    }
    fclose(file);
}

static void setE() //sets the 'e' number for RSA
{
    if(7 < m && m % 7 != 0)
    {
        e = 7;
        return;
    }
    if(17 < m && m % 17 != 0)
    {
        e = 17;
        return;
    }


    FILE* file = fopen("primeNumbers.prime", "r");
    if(file == NULL)
        error("Could not open file for reading the e number");

    while (fscanf(file, "%lli", &e) > 0)
    {
        if(e < m && m % e != 0)
        {
            fclose(file);
            return;
        }
    }

    fclose(file);
    fprintf(stderr, "%s\n", "Failing to set the e number.");
    exit(EXIT_FAILURE);


}

void getPublicKey(int* a, int* b)
{
    *a = (int) n;
    *b = (int) e;
}


static ll power(ll base, ll expo, ll modulo)
{
    ll result = 1;
    while (expo)
    {
        if (expo & 1)
        {
            result *= base;
            result = result % modulo;
        }
        expo >>= 1;
        base *= base;
        base = base % modulo;
    }

    return result;
}

int encrypt(int toEncrypt, ll n, ll e)
{
    return power(toEncrypt, e, n);
}

int decrypt(int toDecrypt)
{
    return power(toDecrypt, d, n);
}

static ll extendedEuclid(ll a, ll b, ll *x, ll *y) //this function is from http://www.thebobblog.com/comment-je-calcule-a-b-c-utilisant-modulaire-inverse-multiplicatif.html
{
    ll t, d;
    if (b == 0)
    {
        *x = 1; *y = 0;
        return a;
    }
    d = extendedEuclid(b, a % b, x, y);
    t = *x;
    *x = *y;
    *y = t - (a/b)*(*y);
    return d;
}

static ll modInverse(ll a, ll n) //this function is from http://www.thebobblog.com/comment-je-calcule-a-b-c-utilisant-modulaire-inverse-multiplicatif.html
{
    ll x, y;
    extendedEuclid(a, n, &x, &y);
    return (x < 0) ? (x + n) : x;
}

void rsaInitialise()
{
    readPrimes();
    n = p * q;
    m = (p - 1) * (q - 1);
    setE();
    d = modInverse(e, m);
}

void decryptBuffer(int* buffer, int length)
{
    int i;
    for(i = 0; i < length; i++)
        buffer[i] = decrypt(buffer[i]);
}

void decryptBufferToChar(int* buffer, int length, char* charBuf)
{
    int i;
    for(i = 0; i < length; i++)
        charBuf[i] = decrypt(buffer[i]);
}
