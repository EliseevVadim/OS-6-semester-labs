ten        db 0ah
hun dw 100
ohun db 101
thous dw 0ff6h
hund db 78h
str db 1ah

idiv thous
idiv hun
idiv bx
iDiv 		cx
idiv  ax
pop dx
pop ds
pop hun
add thous,ax
add   al, ten
	add ax, hun
add thous , cs
add hun   ,   es
add   ten   ,   al
		add bl , ten