
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
              v-model="stores.datatime.data"
              :totalCount="stores.datatime.query.totalCount"
              :columns="stores.datatime.columns"
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
                         v-model="stores.datatime.query.kw"
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
    getdatatimeList,
  } from "@/api/rbac/datatime";
  export default {
    name: "	rbac_datetime_page",
    components: {
      Tables
    },
    data() {
      return {
        stores: {
          datatime: {
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
              { title: "重点工作", key: "no1", width: 200, sortable: true },
             
              { title: "主要举措", key: "no2", width: 200, sortable: true },
              { title: "实施进度", key: "nO3",width: 180, sortable: true,},  
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
      exportData() {
        this.$refs.tables.exportCsv({
          filename: "任务列表",
          original: false,
          columns: this.stores.datatime.columns,
          data: this.stores.datatime.data
        });
      },
      datatimeList() {
        getdatatimeList(this.stores.datatime.query).then(res => {
          this.stores.datatime.data = res.data.data;
          this.stores.datatime.query.totalCount = res.data.totalCount;
        });
      },
      handleRefresh() {
        this.datatimeList();
      },
      handleSearchRole() {
        this.datatimeList();
      },
      rowClsRender(row, index) {
        if (row.isDeleted) {
          return "table-row-disabled";
        }
        return "";
      },
      handlePageChanged(page) {
        this.stores.datatime.query.currentPage = page;
        this.datatimeList();
      },
      handlePageSizeChanged(pageSize) {
        this.stores.datatime.query.pageSize = pageSize;
        this.datatimeList();
      }
    },
    mounted() {
      this.datatimeList();
    }
  };

</script>
