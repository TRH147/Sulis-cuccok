<script setup>

import { computed } from "vue"
import { useRoute } from "vue-router"
import { useMovieStore } from "../stores/movieStore"

const route = useRoute()
const store = useMovieStore()

const movie = computed(() => {
  return store.movies.find(
    m => m.id == route.params.id
  )
})

</script>

<template>

<div class="container mt-5">

<div v-if="movie" class="row">

<div class="col-md-4">

<img
:src="movie.cover"
:alt="movie.title"
class="img-fluid rounded shadow"
/>

</div>

<div class="col-md-8">

<h1 class="mb-3">
{{ movie.title }}
</h1>

<h5 class="text-muted mb-4">
Rendező: {{ movie.director }}
</h5>

<p class="lead">
{{ movie.description }}
</p>

<h4 class="mt-4">
Értékelés
</h4>

<p class="fs-5 text-warning">
{{ '⭐'.repeat(movie.rating) }}
</p>

<div class="progress mb-3" style="height: 30px;">

<div
class="progress-bar"
role="progressbar"
:style="{ width: (movie.rating * 20) + '%' }"
>

{{ movie.rating }}/5

</div>

</div>

</div>

</div>

<div v-else class="text-center">

<h2>A film nem található.</h2>

</div>

</div>

</template>

<style scoped>

img{
    max-height: 600px;
    object-fit: cover;
    width: 100%;
}

</style>