<template>
  <div
    class="home flex-grow-1 d-flex flex-column align-items-center justify-content-center"
  ></div>
</template>

<script>
import { onMounted } from 'vue'
import { api } from '../services/AxiosService'
export default {
  name: 'Home',
  setup() {
    onMounted(async () => {
      const keeps = await api.get('/api/keeps')
      console.log(keeps)
    })
  }
}
</script>

<style scoped lang="scss">
.home {
  text-align: center;
  user-select: none;
  > img {
    height: 200px;
    width: 200px;
  }
}
</style>
