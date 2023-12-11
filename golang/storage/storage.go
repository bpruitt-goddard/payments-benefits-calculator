package storage

import "example/server/types"

type Storage interface {
	Get(int) *types.User
}

