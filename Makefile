build:
	gcc rpicontrol.c -Wall -lwiringPi -o "rpicontrol.out"
run:
	./rpicontrol.out
clear:
	rm rpicontrol.out
