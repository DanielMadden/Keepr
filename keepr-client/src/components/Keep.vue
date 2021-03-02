<template lang="">
  <div class="keep"
       @click="viewKeep()"
  >
    <img class="keep-img" :src="keep.img" />
    <div class="keep-darken"></div>
    <div class="keep-name">
      {{ keep.name }}
    </div>
  </div>
</template>
<script>
import { useRouter } from 'vue-router'
import { AppState } from '../AppState'
import { keepService } from '../services/KeepService'
import { openModal } from '../utils/Modal'
export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const router = useRouter()
    const travel = () => {
      router.push('/keep/' + props.keep.id)
    }
    const viewKeep = () => {
      AppState.activeKeep = props.keep
      keepService.getOne(props.keep.id)
      // TODO open the modal
      openModal('keep')
    }
    return {
      // Variables
      // Functions
      travel,
      viewKeep
    }
  }
}
</script>
<style scoped>
@import "../assets/css/keep.css";
</style>
