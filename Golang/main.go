package main

import (
	"fmt"
	"reflect"
)

func main() {
	name := []int{2, 3, 5, 7, 11, 13}
	const veriabl = 50
	fmt.Print(reflect.TypeOf(name))
}
