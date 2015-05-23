#include <avr/io.h>

#define bf_push(reg) st -X, reg
#define bf_pop(reg) ld reg, X+

.section .data
.skip 128
stack: // grows down

.section .text
.global main
main:
  // put the stack address in X
  ldi XL, lo8(stack)
  ldi XH, hi8(stack)
  rjmp thread
