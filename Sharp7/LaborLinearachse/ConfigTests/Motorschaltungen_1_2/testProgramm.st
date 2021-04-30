FUNCTION_BLOCK TestProgramm
VAR_INPUT
END_VAR
VAR_OUTPUT
END_VAR
VAR
END_VAR

(*  *********************************************************** *)
(*  Linearachse: Motorschaltungen 1 - Version 2                 *)
(*  *********************************************************** *)

IF "DigInput".S1 AND "DigInput".S2 AND "DigInput".S9 THEN
    "DigOutput".P4 := true;
END_IF;

IF NOT "DigInput".S2 OR NOT "DigInput".S9 THEN
    "DigOutput".P4 := FALSE;

END_IF;
"DigOutput".P3 := NOT "DigInput".S9;
    