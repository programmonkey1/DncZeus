 import axios from '@/libs/api.request'

export const gettasklistList = (data) => {
  return axios.request({
    url: 'rbac/tasklist/list',
    method: 'post',
    data
  })
}

// createtasklist
export const createtasklist = (data) => {
  return axios.request({
    url: 'rbac/tasklist/create',
    method: 'post',
    data
  })
}

export const gettasklistSelect = (data) => {
  return axios.request({
    url: 'rbac/tasklist/select',
    method: 'post',
    data
  })
}

//loadtasklist
export const loadtasklist = (data) => {
  return axios.request({
    url: 'rbac/tasklist/edit/' + data.code,
    method: 'get'
  })
}

// edittasklist
export const edittasklist = (data) => {
  return axios.request({
    url: 'rbac/tasklist/edit',
    method: 'post',
    data
  })
}

//load tasklist simple list
export const loadSimpleList = () => {
  return axios.request({
    url: 'rbac/tasklist/find_simple_list',
    method: 'get'
  })
}
