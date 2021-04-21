
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
              v-model="stores.Ceshi.data"
              :totalCount="stores.Ceshi.query.totalCount"
              :columns="stores.Ceshi.columns"
              @on-delete=""
              @on-edit=""
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
                         v-model="stores.Ceshi.query.kw"
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
    getCeshiSelect,
    getRoleList
  } from "@/api/rbac/Ceshi";
  export default {
    name: "	rbac_Ceshi_page",
    components: {
      Tables
    },
    data() {
      return {
        stores: {
          Ceshi: {
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
              { type: "selection", width: 50, key: "handle" },
              { title: "数据库名称", key: "A", width: 250, sortable: true },
              { title: "客户名称", key: "B", width: 200, sortable: true },
              { title: "设备数量", key: "C", width: 100, sortable: true },
              { title: "成功数量", key: "D", width: 100, sortable: true },
              { title: "失败数量", key: "E", width: 100, sortable: true },
              { title: "离线数量", key: "F", width: 100, sortable: true },
              { title: "未知数量", key: "G", width: 100, sortable: true },
              { title: "七天抄表率", key: "G", width: 100, sortable: true },
              
              { title: "一月抄表率", key: "G", width: 100, sortable: true },
              { title: "两月抄表率", key: "G", width: 100, sortable: true },
              { title: "三月抄表率", key: "G", width: 100, sortable: true },
              { title: "今天", key: "G", width: 100, sortable: true },
              { title: "一天前", key: "G", width: 80, sortable: true },
              { title: "两天前", key: "G", width: 80, sortable: true },
              { title: "三天前", key: "G", width: 80, sortable: true },
              { title: "四天前", key: "G", width: 80, sortable: true },
              { title: "五天前", key: "G", width: 80, sortable: true },
              { title: "六天前", key: "G", width: 80, sortable: true },
              { title: "七天前", key: "G", width: 80, sortable: true },
              { title: "一周前", key: "G", width: 80, sortable: true },
              { title: "两周前", key: "G", width: 80, sortable: true },
              { title: "三周前", key: "G", width: 80, sortable: true },
              { title: "一月前", key: "G", width: 80, sortable: true },
              { title: "两月前", key: "G", width: 80, sortable: true },
              { title: "三月前", key: "G", width: 80, sortable: true },
              
              { title: "正在通讯", key: "G", width: 100, sortable: true },
              { title: "占线", key: "G", width: 80, sortable: true },
              
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
      Ceshielect() {
        getCeshiSelect(this.stores.Ceshi.query).then(res => {
          this.stores.Ceshi.data = res.data.data;
          this.stores.Ceshi.query.totalCount = res.data.totalCount;
        });
      },
      exportData() {
        this.$refs.tables.exportCsv({
          filename: "测试",
          original: false,
          columns: this.stores.Ceshi.columns,
          data: this.stores.Ceshi.data
        });
      },
      CeshiList() {
        getRoleList(this.stores.Ceshi.query).then(res => {
          this.stores.Ceshi.data = res.data.data;
          this.stores.Ceshi.query.totalCount = res.data.totalCount;
        });
      },
      handleRefresh() {
        this.CeshiSelect();
      },
      handleSearchRole() {
        this.CeshiSelect();
      },
      rowClsRender(row, index) {
        if (row.isDeleted) {
          return "table-row-disabled";
        }
        return "";
      },
      handlePageChanged(page) {
        this.stores.Ceshi.query.currentPage = page;
        this.CeshiList();
      },
      handlePageSizeChanged(pageSize) {
        this.stores.Ceshi.query.pageSize = pageSize;
        this.CeshiList();
      }
    },
    mounted() {
      this.CeshiList();
    }
  };

</script>
