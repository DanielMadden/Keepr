<template lang="">
  <div class="row"
       id="modal-keep-container"
  >
    <div class="col-6"
         id="modal-keep-img"
         :style="`background: linear-gradient( rgba(0, 0, 0, 0.0), rgba(0, 0, 0, 0)), url('${keep.img}') no-repeat center center /cover; overflow-y: hidden`"
    ></div>
    <div class="col-6"
         id="modal-keep-content"
    >
      <div class="row d-flex justify-content-end"
           id="modal-keep-row-x"
      >
        X
      </div>
      <div class="row d-flex justify-content-center mb-3 align-items-center"
           id="modal-keep-row-stats"
      >
        <div class="modal-keep-stats">
          <i class="fas fa-eye"></i> {{ keep.views }}
        </div>
        <div class="modal-keep-stats mx-5">
          <i class="fab fa-kickstarter"></i> {{ keep.keeps }}
        </div>
        <div class="modal-keep-stats">
          <i class="fas fa-share-alt"></i> {{ keep.shares }}
        </div>
      </div>
      <div class="row d-flex justify-content-center mb-3 pt-2"
           id="modal-keep-row-title"
      >
        <h1>
          {{ keep.name }}
        </h1>
      </div>
      <div class="row"
           id="modal-keep-row-description"
      >
        <p class="m-0"
           id="modal-keep-description"
        >
          Lorem ipsum aowudhawuohdawudaiudhawiydgwd79awy awdawdahw dawd80ahw0d8h8 0182h 08wdh awodh 102d dhuawd8 h0812y 08yda0 8dhawdh l182 8awd hawiudh 812yd ahduiw lawud hlawud h9821 98ahdwiahlwduh1 028 dhawdh aiwhld 9812d hawld aiwd lauwdh 0182dy 8awh dialwd u1h2 08ahwdl awdu h102 wauhdlaw dhliawhd 0182hd aiwdl auiwdh 12h duahw diawhd 8102d hauwhdl awudh 0182hd uiwalhd uiwahd 1y2d
        </p>
      </div>
      <div class="row d-flex justify-content-center"
           id="modal-keep-row-tags"
      >
        <div class="d-flex justify-content-center mb-5 mt-5 pt-3"
             id="modal-keep-tags"
        >
          Hi
          {{ keep.tags }}
        </div>
      </div>
      <div class="row d-flex justify-content-between mt-3"
           id="modal-keep-row-bottom"
      >
        <div id="modal-keep-add">
          Add To Vault
        </div>
        <div id="modal-keep-delete">
          <i class="fa fa-trash"></i>
        </div>
        <div id="modal-keep-profile"
             class="d-flex justify-content-end align-items-center"
        >
          <div id="modal-keep-profile-picture"
               class="mr-2"
               :style="`background: 0, 0, url('${keep.creator.picture}') no-repeat center center /cover; overflow-y: hidden`"
          ></div>
          <div id="modal-keep-profile-name">
            {{ keep.creator.name }}
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
export default {
  setup() {
    const keep = computed(() => AppState.activeKeep)
    return {
      // Variables
      keep
      // Functions
    }
  }
}
</script>
<style scoped>
@import "../assets/css/modal-keep.css";
</style>
