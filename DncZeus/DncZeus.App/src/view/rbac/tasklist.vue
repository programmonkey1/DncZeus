
<template>
  <div>
    <Card>
      <tables ref="tables"
              editable
              searchable
               height="600" 
              :border="false"
              size="small"
              search-place="top"
              v-model="stores.tasklist.data"
              :totalCount="stores.tasklist.query.totalCount"
              :columns="stores.tasklist.columns"
              @on-delete="handleDelete"
              @on-edit="handleEdit"
              @on-select="handleSelect"
              @on-selection-change="handleSelectionChange"
              @on-refresh="handleRefresh"
              :row-class-name="rowClsRender"
              @on-page-change="handlePageChanged"
              @on-page-size-change="handlePageSizeChanged">
        <div slot="search">
          <section class="dnc-toolbar-wrap">
            <Row :gutter="16">
              <Col span="4">
              <Form inline @submit.native.prevent>
                <FormItem>
                  <Input type="text"
                         search
                         :clearable="true"
                         v-model="stores.tasklist.query.kw"
                         placeholder="输入关键字搜索..."
                         @on-search="handleSearchRole()">

                  </Input>
                </FormItem>
              </Form>
              </Col>
              <Col span="1">
              <Button type="primary"
                      icon="md-create"
                      @click="exportData"
                      title="导出">
                导出
              </Button>
              </Col>
              <Col span="18" class="dnc-toolbar-btns">
              <ButtonGroup class="mr3">

                <Button icon="md-refresh" title="刷新" @click="handleRefresh"></Button>
              </ButtonGroup>

              </Col>
            </Row>
          </section>
        </div>
      </tables>
    </Card>

  </div>
</template>


<script>
import Tables from "_c/tables";
import {
    gettasklistSelect,
    gettasklistList,
  } from "@/api/rbac/tasklist";
  export default {
    name: "	rbac_tasklist_page",
    components: {
      Tables
    },
    data() {
      return {
        stores: {
          tasklist: {
            query: {
              totalCount: 0,
              pageSize: 20,
              currentPage: 1,
              kw: "",
              isDeleted: 0,
              status: -1,
              sort: [
                {
                  direct: "DESC",
                  field: "CreatedOn"
                }
              ]
            },
            sources: {
              isDeletedSources: [
                { value: -1, text: "全部" },
                { value: 0, text: "正常" },
                { value: 1, text: "已删" }
              ],
              statusSources: [
                { value: -1, text: "全部" },
                { value: 0, text: "禁用" },
                { value: 1, text: "正常" }
              ],
              statusFormSources: [
                { value: 0, text: "禁用" },
                { value: 1, text: "正常" }
              ]
            },
            columns: [
              { type: "selection", width: 40, key: "handle" },
              { title: "序号", key: "id", width: 70, sortable: true },
              { title: "重点工作", key: "keyWork", width: 200, sortable: true },
              { title: "工作重要程度", 
              key: "importanceOfWork", 
              width: 150, sortable: true,
              render:(h,params)=>{
                        let importanceOfWork = params.row.importanceOfWork
                        if(importanceOfWork==0){
                            importanceOfWork = '重要工作'
                        }else if(importanceOfWork==1){
                            importanceOfWork = '一般工作'
                        }else if(importanceOfWork ==2){
                            importanceOfWork = '次要工作'
                        }
                        return h('span',importanceOfWork)
                    }
              },
              { title: "主要举措", key: "majorIinitiatives", width: 200, sortable: true },
              { title: "实施进度", key: "implementationProgress",width: 180, sortable: true,},   
              { title: "进展状态", 
              key: "progressStatus", 
              width: 100, sortable: true,
              render:(h,params)=>{
                        let progressStatus = params.row.progressStatus
                        if(progressStatus==0){
                            progressStatus = '提前/按期完成'
                        }else if(progressStatus==1){
                            progressStatus = '滞后完成'
                        }else if(progressStatus ==2){
                            progressStatus = '按计划完成'
                        }else if(progressStatus ==3){
                            progressStatus = '有所延期'
                        }else if(progressStatus ==4){
                            progressStatus = '严重延期'
                        }
                        return h('span',progressStatus)
                    },
              },
              { title: "负责人", key: "personLiable", width: 100, sortable: true },
              { title: "备注", key: "remarks", width: 100, sortable: true },
              { title: "创建时间", key: "creationTime", width: 150, sortable: true },
              { title: "完成情况",
               key: "isComplete",
                width: 100, 
                sortable: true,
                render:(h,params)=>{
                        let isComplete = params.row.isComplete
                        if(isComplete==0){
                            isComplete = '完成'
                        }else if(isComplete==1){
                            isComplete = '未完成'
                        }
                        return h('span',isComplete)
                    },
              },
              {
                title: "操作",
                align: "center",
                key: "handle",
                width: 150,
                className: "table-command-column",
                options: ["edit"],
                button: [
                  (h, params, vm) => {
                    return h(
                      "Poptip",
                      {
                        props: {
                          confirm: true,
                          title: "你确定要删除吗?"
                        },
                        on: {
                          "on-ok": () => {
                            vm.$emit("on-delete", params);
                          }
                        }
                      },
                      [
                        h(
                          "Tooltip",
                          {
                            props: {
                              placement: "left",
                              transfer: true,
                              delay: 1000
                            }
                          },
                          [
                            h("Button", {
                              props: {
                                shape: "circle",
                                size: "small",
                                icon: "md-trash",
                                type: "error"
                              }
                            }),
                            h(
                              "p",
                              {
                                slot: "content",
                                style: {
                                  whiteSpace: "normal"
                                }
                              },
                              "删除"
                            )
                          ]
                        )
                      ]
                    );
                  },
                  (h, params, vm) => {
                    return h(
                      "Tooltip",
                      {
                        props: {
                          placement: "left",
                          transfer: true,
                          delay: 1000
                        }
                      },
                      [
                        h("Button", {
                          props: {
                            shape: "circle",
                            size: "small",
                            icon: "md-create",
                            type: "primary"
                          },
                          on: {
                            click: () => {
                              vm.$emit("on-edit", params);
                              vm.$emit("input", params.tableData);
                            }
                          }
                        }),
                        h(
                          "p",
                          {
                            slot: "content",
                            style: {
                              whiteSpace: "normal"
                            }
                          },
                          "编辑"
                        )
                      ]
                    );
                  }
                ]
              }
            ],
            data: []
          }
        },
        styles: {
          height: "calc(100% - 55px)",
          overflow: "auto",
          paddingBottom: "53px",
          position: "static"
        }
      }
    },
    methods: {
      tasklistSelect() {
        gettasklistSelect(this.stores.tasklist.query).then(res => {
          this.stores.tasklist.data = res.data.data;
          this.stores.tasklist.query.totalCount = res.data.totalCount;
        });
      },
      exportData() {
        this.$refs.tables.exportCsv({
          filename: "任务列表",
          original: false,
          columns: this.stores.tasklist.columns,
          data: this.stores.tasklist.data
        });
      },
      tasklistList() {
        gettasklistList(this.stores.tasklist.query).then(res => {
          this.stores.tasklist.data = res.data.data;
          this.stores.tasklist.query.totalCount = res.data.totalCount;
        });
      },
      handleRefresh() {
        this.tasklistSelect();
      },
      handleSearchRole() {
        this.tasklistSelect();
      },
      rowClsRender(row, index) {
        if (row.isDeleted) {
          return "table-row-disabled";
        }
        return "";
      },
      handlePageChanged(page) {
        this.stores.tasklist.query.currentPage = page;
        this.tasklistList();
      },
      handlePageSizeChanged(pageSize) {
        this.stores.tasklist.query.pageSize = pageSize;
        this.tasklistList();
      }
    },
    mounted() {
      this.tasklistList();
    }
  };

</script>
