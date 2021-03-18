FUNCTION_BLOCK TestProgramm
VAR_INPUT
END_VAR
VAR_OUTPUT
END_VAR
VAR
    ZyklusAktiv: BOOL;
	Zeit: TON;
END_VAR

(*  *********************************************************** *)
(*  LAP 2010/1 Kompressoranlage                                 *)
(*  *********************************************************** *)

IF "DigInput".S1 THEN
    #ZyklusAktiv := true;
END_IF;

