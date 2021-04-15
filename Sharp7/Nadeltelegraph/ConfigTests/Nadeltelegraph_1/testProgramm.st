FUNCTION_BLOCK TestProgramm
VAR_INPUT
END_VAR
VAR_OUTPUT
END_VAR
VAR
    Bitmuster: ARRAY[0..256] of Word;
    Init: BOOL;
    Zeiger: Int;
    Ascii: Word;
    Ergebnis: Word;
END_VAR
CONST
    P1R: Word: 2#0000_0000_0001;
    P1L: Word: 2#0000_0000_0010;
    P2R: Word: 2#0000_0000_0100;
    P2L: Word: 2#0000_0000_1000;
    P3R: Word: 2#0000_0001_0000;
    P3L: Word: 2#0000_0010_0000;
    P4R: Word: 2#0000_0100_0000;
    P4L: Word: 2#0000_1000_0000;
    P5R: Word: 2#0001_0000_0000;
    P5L: Word: 2#0010_0000_0000;
END_CONST

(*  *********************************************************** *)
(*  Nadeltelegraph                                              *)
(*  *********************************************************** *)



IF NOT #Init THEN
    FOR #Zeiger:= 0 TO  255 DO
        #BitMuster[#Zeiger] := 0;
    END_FOR;
    
    #BitMuster[16#41] := #P1R + #P5L; //65 A
    #BitMuster[16#42] := #P1R + #P4L; //66 B
    #BitMuster[16#44] := #P2R + #P5L; //68 D
    #BitMuster[16#45] := #P1R + #P3L; //69 E
    #BitMuster[16#46] := #P2R + #P4L; //70 F
    #BitMuster[16#47] := #P3R + #P5L; //71 G
    #BitMuster[16#48] := #P1R + #P2L; //72 H
    #BitMuster[16#49] := #P2R + #P3L; //73 I
    #BitMuster[16#4B] := #P3R + #P4L; //75 K
    #BitMuster[16#4C] := #P4R + #P5L; //76 L
    
    #BitMuster[16#4D] := #P1L + #P2R; //77 M
    #BitMuster[16#4E] := #P2L + #P3R; //78 N
    #BitMuster[16#4F] := #P3L + #P4R; //79 O
    #BitMuster[16#50] := #P4L + #P5R; //80 P
    #BitMuster[16#52] := #P1L + #P3R; //82 R
    #BitMuster[16#53] := #P2L + #P4R; //83 S
    #BitMuster[16#54] := #P3L + #P5R; //84 T
    #BitMuster[16#56] := #P1L + #P4R; //86 V
    #BitMuster[16#57] := #P2L + #P5R; //87 W
    #BitMuster[16#59] := #P1L + #P5R; //89 Y    
END_IF;

#Ascii := "DigInput".ASCII;
#Ergebnis := #BitMuster[#Ascii];

POKE(area := 16#84, dbNumber := 3, byteOffset := 0, value := #Ergebnis.%B0);
POKE(area := 16#84, dbNumber := 3, byteOffset := 1, value := #Ergebnis.%B1);