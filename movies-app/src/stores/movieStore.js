import { defineStore } from "pinia"
import axios from "axios"

export const useMovieStore = defineStore("movies", {
  state: () => ({
    movies: []
  }),

  actions: {
    async fetchMovies() {
      const res = await axios.get("http://localhost:3000/movies")
      this.movies = res.data
    }
  }
})