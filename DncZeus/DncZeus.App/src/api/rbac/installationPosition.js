import axios from '@/libs/api.request'

export const getRoleList = (data) => {
  return axios.request({
    url: 'rbac/installationposition/list',
    method: 'post',
    data
  })
}

// createRole
export const createRole = (data) => {
  return axios.request({
    url: 'rbac/installationposition/create',
    method: 'post',
    data
  })
}

//loadRole
export const loadRole = (data) => {
  return axios.request({
    url: 'rbac/installationposition/edit/' + data.ipid,
    method: 'get'
  })
}

// editRole
export const editRole = (data) => {
  return axios.request({
    url: 'rbac/installationposition/edit',
    method: 'post',
    data
  })
}

// delete role
export const deleteRole = (ids) => {
  return axios.request({
    url: 'rbac/installationposition/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'rbac/installationposition/batch',
    method: 'get',
    params: data
  })
}


// loadEcutableDataSource

// find Ecutable data source by keyword
export const findinstallationpositionDataSourceByKeyword = (data) => {
  return axios.request({
    url: 'rbac/installationposition/find_list_by_kw/' + data.keyword,
    method: 'get'
  })
}

