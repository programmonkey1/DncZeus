import axios from '@/libs/api.request'

export const getWorkTaskList = (data) => {
  return axios.request({
    url: 'rbac/worktask/list',
    method: 'post',
    data
  })
}

// createWorkTask
export const createWorkTask = (data) => {
  return axios.request({
    url: 'rbac/worktask/create',
    method: 'post',
    data
  })
}

//loadWorkTask
export const loadWorkTask = (data) => {
  return axios.request({
    url: 'rbac/worktask/edit/' + data.id,
    method: 'get'
  })
}

// editRole
export const editWorkTask = (data) => {
  return axios.request({
    url: 'rbac/worktask/edit',
    method: 'post',
    data
  })
}

// delete role
export const deleteWorkTask = (ids) => {
  return axios.request({
    url: 'rbac/worktask/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'rbac/worktask/batch',
    method: 'get',
    params: data
  })
}

// loadEcutableDataSource

// find Ecutable data source by keyword
export const findworktaskDataSourceByKeyword = (data) => {
  return axios.request({
    url: 'rbac/worktask/find_list_by_kw/' + data.keyword,
    method: 'get'
  })
}
