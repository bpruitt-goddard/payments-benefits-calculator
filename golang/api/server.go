package api

import (
	"example/server/storage"
	"example/server/utils"
	"net/http"
)

type Server struct {
	listenAddr string
	store storage.Storage
}

func NewServer(listenAddr string, store storage.Storage) *Server {
	return &Server{
		listenAddr: listenAddr,
		store: store,
	}
}

func (s *Server)Start() error {
	http.HandleFunc("/users", s.handleGetUserById)
	return http.ListenAndServe(s.listenAddr, nil)
}

func (s *Server) handleGetUserById(w http.ResponseWriter, r *http.Request) {
	user := s.store.Get(10)

	utils.WriteJson(w, user)
}
