<template>
  <div class="c-app">
    <transition name="fade" mode="out-in">
        <router-view :key="$route.path"></router-view>
    </transition>
  </div>
</template>

<script>

export default {
  name: 'Container'
}
</script>