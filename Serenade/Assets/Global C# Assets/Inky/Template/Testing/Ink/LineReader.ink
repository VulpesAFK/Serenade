How peculiar ... 
A guest here at this time ...
-> Third_line

=== Third_line === 
Lilithé is my name
What business do you have with me?
    + [Rosemondé, here to inquire about this fog]
        -> Answer(1)
    + [Just simply got lost]
        -> Forth_line

=== Forth_line ===
Lost? Then little lamb, let me welcome you till we settle this issue
    + [Thank you very much]
        -> Answer(2)
    + [How kind]
        -> Answer(3)

=== Answer(number) ===
You chose option {number} 
-> END
