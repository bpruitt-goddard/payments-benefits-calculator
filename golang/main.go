package main

import (
	"example/server/api"
	"example/server/storage"
	"flag"
	"fmt"
	"log"
)

func main() {
	listenAddr := flag.String("listenaddr", ":3000", "the server address")
	flag.Parse()
	
	store := storage.NewMemoryStorage()

	server := api.NewServer(*listenAddr, store)
	fmt.Println("server running on port:", *listenAddr)
	log.Fatal(server.Start())
}