 /**
 * PS Move API - An interface for the PS Move Motion Controller
 * Copyright (c) 2011 Thomas Perl <m@thp.io>
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *
 *    1. Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *
 *    2. Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE
 * LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
 * CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
 * SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
 * CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
 * POSSIBILITY OF SUCH DAMAGE.
 **/



#include <unistd.h>
#include <stdio.h>
#include <stdlib.h>

#include "psmove.h"

int main(int argc, char* argv[])
{
    printf("TONY TONY\n");
    PSMove *move;
    int off = 0;
    if (argc > 1) {
      off = atoi(argv[1]);
    }
    printf ("Rumble: %d\n", off);
    enum PSMove_Connection_Type ctype;
    int i;

    if (!psmove_init(PSMOVE_CURRENT_VERSION)) {
        fprintf(stderr, "PS Move API init failed (wrong version?)\n");
        exit(1);
    }

    i = psmove_count_connected();
    printf("Connected controllers: %d\n", i);

    move = psmove_connect();

    if (move == NULL) {
        printf("Could not connect to default Move controller.\n"
               "Please connect one via USB or Bluetooth.\n");
        exit(1);
    }

    char *serial = psmove_get_serial(move);
    printf("Serial: %s\n", serial);
    free(serial);

    ctype = psmove_connection_type(move);
    switch (ctype) {
        case Conn_USB:
            printf("Connected via USB.\n");
            break;
        case Conn_Bluetooth:
            printf("Connected via Bluetooth.\n");
            break;
        case Conn_Unknown:
            printf("Unknown connection type.\n");
            break;
    }

/*
    for (i=0; i<10; i++) {
        psmove_set_leds(move, 0, 255*(i%3==0), 0);
        psmove_set_rumble(move, 255*(i%2));
        psmove_update_leds(move);
        usleep(10000*(i%10));
    }

    for (i=250; i>=0; i-=5) {
        psmove_set_leds(move, i, i, 0);
        if (!off)
          psmove_set_rumble(move, 0);
        psmove_update_leds(move);
    }
*/
    /* Enable rate limiting for LED updates */
    psmove_set_rate_limiting(move, 1);

    psmove_set_leds(move, 0, 0, 0);
    if (!off)
    	psmove_set_rumble(move, 0);
    else
    	psmove_set_rumble(move, off);
    psmove_update_leds(move);

    while (ctype != Conn_USB && !(psmove_get_buttons(move) & Btn_PS)) {
        int res = psmove_poll(move);
        if (res) {
	    FILE * f = fopen("commands.txt", "r");
 	    printf("opening file... \n");
            if (f != NULL){
		int rumble = 0;
		fscanf(f,"%d",&rumble);
    	        psmove_set_rumble(move, rumble);
		printf("Setting rumble to : %d\n", rumble);
		fclose(f);
	    }
	    sleep(1);
            psmove_update_leds(move);
        }
    }

    psmove_disconnect(move);

    return 0;
}

