<script setup>

import { onMounted } from "vue"
import { useRouter } from "vue-router"
import { useMovieStore } from "../stores/movieStore"

const store = useMovieStore()
const router = useRouter()

onMounted(() => {
  store.fetchMovies()
})

function showDetails(id) {
  router.push(`/details/${id}`)
}

</script>

<template>

<div class="container mt-4">

<h2 class="mb-4">Filmek</h2>

<div class="row">

<div
v-for="movie in store.movies"
:key="movie.id"
class="col-md-3 mb-4"
>

<div class="card h-100 shadow">

<img
:src="movie.cover"
class="card-img-top"
/>

<div class="card-body d-flex flex-column">

<h5 class="card-title">
{{ movie.title }}
</h5>

<p class="text-warning fs-5">
{{ '⭐'.repeat(movie.rating) }}
</p>

<button
class="btn btn-primary mt-auto"
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

<style scoped>

.card-img-top{
    height: 400px;
    object-fit: cover;
}

</style>