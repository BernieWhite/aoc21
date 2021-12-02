package main

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
)

func main() {
	// Q1()
	// Q2()
	// Q3()
	Q4()
}

// Day 1

func Input1() []int {
	file, err := os.Open("./input1.txt")
	if err != nil {
		fmt.Println(err)
	}
	defer file.Close()

	scanner := bufio.NewScanner(file)
	var values []int
	for scanner.Scan() {
		n, err := strconv.Atoi(scanner.Text())
		values = append(values, n)
		if err != nil {
			fmt.Println(err)
		}
	}
	return values
}

func Q1() {
	m := Input1()
	count := 0
	last := m[0]
	for i := 0; i < len(m); i++ {
		if m[i] > last {
			count++
		}
		last = m[i]
	}
	fmt.Printf("Q1: %v", count)
}

func Q2() {
	m := Input1()
	count := 0
	i := 3
	mm := sum(m[0:i])
	for last := mm; i <= len(m); mm = sum(m[i-3 : i]) {
		if mm > last {
			count++
		}
		last = mm
		i++
	}
	fmt.Printf("Q2: %v", count)
}

func sum(val []int) int {
	result := 0
	for _, v := range val {
		result += v
	}
	return result
}

// Day 2

func Input2() [][2]int {
	file, err := os.Open("./input2.txt")
	if err != nil {
		fmt.Println(err)
	}
	defer file.Close()

	scanner := bufio.NewScanner(file)
	scanner.Split(bufio.ScanWords)
	var values [][2]int
	for scanner.Scan() {
		d := scanner.Text()
		scanner.Scan()
		n, err := strconv.Atoi(scanner.Text())
		if err != nil {
			fmt.Println(err)
		}

		switch d {
		case "up":
			values = append(values, [2]int{0, -n})
		case "down":
			values = append(values, [2]int{0, n})
		case "forward":
			values = append(values, [2]int{n, 0})
		}

		if err != nil {
			fmt.Println(err)
		}
	}
	return values
}

func Q3() {
	d := Input2()
	x := 0
	y := 0
	for i := 0; i < len(d); i++ {
		x += d[i][0]
		y += d[i][1]
	}
	fmt.Printf("Q3: %v", x*y)
}

func Q4() {
	d := Input2()
	x := 0
	depth := 0
	aim := 0
	for i := 0; i < len(d); i++ {
		aim += d[i][1]
		if d[i][0] > 0 {
			x += d[i][0]
			depth += d[i][0] * aim
		}
	}
	fmt.Printf("Q4: %v", x*depth)
}
