(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-7b0351f8"],{6326:function(e,a,t){},"78a7":function(e,a,t){"use strict";t.r(a);var c=t("7a23");const i=Object(c["M"])("data-v-3eea795e");Object(c["w"])("data-v-3eea795e");const n={class:"viewport"},s={class:"masonry-3"},d={class:"viewport"},o=Object(c["g"])('<div class="loader-page-container" data-v-3eea795e><div class="loader-page-spinner d-flex justify-content-center align-items-center animate-in" data-v-3eea795e><i class="fas fa-certificate slow-spin" data-v-3eea795e></i></div><div class="loader-page-logo no-stretch d-flex justify-content-center align-items-center animate-in" data-v-3eea795e><i class="fab fa-kickstarter-k" data-v-3eea795e></i></div></div>',1);Object(c["u"])();const l=i((e,a,t,i,l,r)=>{const b=Object(c["A"])("Keep");return Object(c["t"])(),Object(c["e"])("div",null,[Object(c["K"])(Object(c["i"])("div",n,[Object(c["i"])("div",s,[(Object(c["t"])(!0),Object(c["e"])(c["a"],null,Object(c["z"])(i.keeps,e=>(Object(c["t"])(),Object(c["e"])(b,{key:e.id,keep:e},null,8,["keep"]))),128))])],512),[[c["H"],i.loaded&&!i.animating]]),Object(c["K"])(Object(c["i"])("div",d,[o],512),[[c["H"],i.animating||!i.loaded]])])});var r=t("4e2d"),b=t("83fc"),j={name:"Home",setup(){const e=Object(c["c"])(()=>b["a"].homeKeeps),a=Object(c["c"])(()=>b["a"].loaded.home),t=Object(c["c"])(()=>b["a"].animating.home);return Object(c["r"])(async()=>{b["a"].animating.home=!0,await r["a"].getAll(),setTimeout(()=>{b["a"].loaded.home=!0},1e3),setTimeout(()=>{b["a"].animating.home=!1},1e3)}),{keeps:e,loaded:a,animating:t}}};t("eafb");j.render=l,j.__scopeId="data-v-3eea795e";a["default"]=j},eafb:function(e,a,t){"use strict";t("6326")}}]);