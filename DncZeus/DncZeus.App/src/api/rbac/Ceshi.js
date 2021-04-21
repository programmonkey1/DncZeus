import axios from '@/libs/api.request'

export const getCeshiSelect = (data) => {
  return axios.request({
    url: 'rbac/Ceshi/list',
    method: 'post',
    data
  })
}

// createRole
export const createRole = (data) => {
  return axios.request({
    url: 'rbac/Ceshi/create',
    method: 'post',
    data
  })
}

//loadRole
export const loadRole = (data) => {
  return axios.request({
    url: 'rbac/Ceshi/edit/' + data.btid,
    method: 'get'
  })
}

// editRole
export const editRole = (data) => {
  return axios.request({
    url: 'rbac/Ceshi/edit',
    method: 'post',
    data
  })
}

// delete role
export const deleteRole = (ids) => {
  return axios.request({
    url: 'rbac/Ceshi/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'rbac/Ceshi/batch',
    method: 'get',
    params: data
  })
}

// loadbasetableDataSource

// find basetable data source by keyword
export const findbasetableDataSourceByKeyword = (data) => {
  return axios.request({
    url: 'rbac/Ceshi/find_list_by_kw/' + data.keyword,
    method: 'get'
  })
}



