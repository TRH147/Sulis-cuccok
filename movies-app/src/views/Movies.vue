<script setup>

import { onMounted } from "vue"
import { useMovieStore } from "../stores/movieStore"

const store = useMovieStore()

onMounted(() => {
  store.fetchMovies()
})

function showDetails(id) {
  console.log("Film ID:", id)
}

</script>

<template>

<div class="container mt-4">

<h2>Filmek</h2>

<div class="row">

<div
v-for="movie in store.movies"
:key="movie.id"
class="col-md-3 mb-4"
>

<div class="card h-100">

<img
:src="movie.cover"
class="card-img-top"
/>

<div class="card-body">

<h5 class="card-title">
{{ movie.title }}
</h5>

<button
class="btn btn-primary"
@click="showDetails(movie.id)"
>
Részletek
</button>

</div>

</div>

</div>

</div>

</div>

</template>