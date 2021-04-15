FUNCTION_BLOCK TestProgramm
VAR_INPUT
END_VAR
VAR_OUTPUT
END_VAR
VAR
	Zeit: TON;
END_VAR

(*  *************************************************************************************** *)
(*  Linearachse: Motorschaltungen 2 - Direktes Einschalten mit Reversieren Version 1        *)
(*  *************************************************************************************** *)

IF "DigInput".S1 THEN   "DigOutput".P4 := TRUE;     END_IF;
IF #Zeit.Q THEN         "DigOutput".P4 := FALSE;    END_IF;

#Zeit.TON(IN :="DigOutput".P4 , PT := T#4S);

"DigOutput".Q1 := (#Zeit.ET < T#2s) AND "DigOutput".P4;
"DigOutput".Q2 := (#Zeit.ET > T#2S) AND "DigOutput".P4;
     