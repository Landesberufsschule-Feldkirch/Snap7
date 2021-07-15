FUNCTION_BLOCK TestProgramm
VAR_INPUT
END_VAR
VAR_OUTPUT
END_VAR
VAR
END_VAR

(*  *********************************************************** *)
(*  Linearachse: Motorschaltungen 1 - Version 1                 *)
(*  *********************************************************** *)

IF "DigInput".S1 THEN
    "DigOutput".P4 := true;
END_IF;

IF NOT "DigInput".S2 THEN
    "DigOutput".P4 := FALSE;
END_IF;
    