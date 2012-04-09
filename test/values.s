set a, 0x1000
set a, b
set a, [0x1000]
:here
set a, here
set [a], 0x1000
set [a], b
set [a], [0x1000]
set [a+0x10], [0x10+a]
set push, 0x1000
set c, pop
set sp, peep
