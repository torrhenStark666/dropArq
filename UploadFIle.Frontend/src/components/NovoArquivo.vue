<template>
  <div class="centered-container">
    <form novalidate class="md-layout" @submit.prevent="validateFile">
      <md-card class="md-layout-item md-size-200 md-small-size-100 ">
        <md-card-header>
          <div class="md-title">Novo Upload</div>
          Anexe seu arquivo a seguir:
        </md-card-header>

        <md-card-content>
          <div class="md-layout">
            <div md-layout-item md-size-200 md-small-size-100>
              <md-field >
                <label>Single</label>
                <md-file @md-change="onFileUpload($event)" />
                <span class="md-error" v-if="!$v.file.required">Campo Obrigatório</span>
              </md-field>
            </div>
          </div>
        </md-card-content>

        <md-progress-bar md-mode="indeterminate" v-if="sending" />

        <md-card-actions>
          <md-button type="submit" class="md-primary" :disabled="sending">Pronto!</md-button>
        </md-card-actions>
      </md-card>
    </form>
  </div>
</template>

<script>
  import { validationMixin } from 'vuelidate'
  import {
    required
  } from 'vuelidate/lib/validators'

  export default {
    name: 'NewFile',
    mixins: [validationMixin],
    data: () => ({
      file: null,
      sending: false
    }),
    validations: {
      file : required
      }
    ,
    methods: {
      
      clearForm () {
        this.$v.$reset()
        this.file = null
      },
      onFileUpload (evt) {
        this.file = evt[0]
      },
      save () {
        this.sending = true
        let formData = new FormData();
        formData.append('file', this.file);
        this.$axios
          .post('http://localhost:5000/api/Document/upload', formData,
          {
            headers: {
                'Content-Type': 'multipart/form-data'
          }
  })
          .then( response =>{
            console.log(response)
            this.$router.push('/Dashboard');
          })
          .catch( err =>{
            console.log(err);
            alert('Erro ao fazer upload de arquivo')
          })
          .finally( () =>{
            this.sending = false;
          })
      },
      validateFile () {
        this.$v.$touch()

        if (!this.$v.$invalid) {
          this.save()
        }
      }
    },
    created () {
      if(!localStorage.token){
        this.$router.push('/');
        
        return;
      }
    }
  }
</script>

<style lang="scss" scoped>
  .md-progress-bar {
    position: absolute;
    top: 0;
    right: 0;
    left: 0;
  }
  .centered-container {
  display: flex;
  align-items: center;
  justify-content: center;
  position: relative;
  height: 75vh;
  }
</style>
