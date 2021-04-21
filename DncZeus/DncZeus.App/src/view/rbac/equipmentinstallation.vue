<template>
  <div>
    <Card>
      <tables ref="tables"
              editable
              searchable
              :border="false"
              size="small"
              search-place="top"
              v-model="stores.equipmentinstallation.data"
              :totalCount="stores.equipmentinstallation.query.totalCount"
              :columns="stores.equipmentinstallation.columns"
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
                         v-model="stores.equipmentinstallation.query.kw"
                         placeholder="输入关键字搜索..."
                         @on-search="handleSearchRole()">
                  <Select slot="prepend"
                          v-model="stores.equipmentinstallation.query.isDeleted"
                          @on-change="handleSearchRole"
                          placeholder="删除状态"
                          style="width:60px;">
                    <Option v-for="item in stores.equipmentinstallation.sources.isDeletedSources"
                            :value="item.value"
                            :key="item.value">
                      {{item.text}}
                    </Option>
                  </Select>
                  <Select slot="prepend"
                          v-model="stores.equipmentinstallation.query.status"
                          @on-change="handleSearchRole"
                          placeholder="角色状态"
                          style="width:60px;">
                    <Option v-for="item in stores.equipmentinstallation.sources.statusSources"
                            :value="item.value"
                            :key="item.value">
                      {{item.text}}
                    </Option>
                  </Select>
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
              <Col span="1">
              <Upload ref="upload"
                      action="/api/book/excel/import"
                      name="excel-file"
                      :show-upload-list="true"
                      :on-format-error="handleFormatError"
                      :on-success="handleSuccess"
                      :on-error="handleError"
                      :format="['xlsx','xls']">
                <Button type="primary" icon="ios-cloud-upload-outline">批量导入</Button>
              </Upload>
              </Col>
              <Col span="18" class="dnc-toolbar-btns">
              <ButtonGroup class="mr3">
                <Button class="txt-danger"
                        icon="md-trash"
                        title="删除"
                        @click="handleBatchCommand('delete')"></Button>
                <Button class="txt-success"
                        icon="md-redo"
                        title="恢复"
                        @click="handleBatchCommand('recover')"></Button>
                <Button class="txt-danger"
                        icon="md-hand"
                        title="禁用"
                        @click="handleBatchCommand('forbidden')"></Button>
                <Button class="txt-success"
                        icon="md-checkmark"
                        title="启用"
                        @click="handleBatchCommand('normal')"></Button>
                <Button icon="md-refresh" title="刷新" @click="handleRefresh"></Button>
              </ButtonGroup>
              <Button icon="md-create"
                      type="primary"
                      @click="handleShowCreateWindow"
                      title="新增角色">
                新增角色
              </Button>
              </Col>
            </Row>
          </section>
        </div>
      </tables>
    </Card>
    <Drawer :title="formTitle"
            v-model="formModel.opened"
            width="600"
            :mask-closable="true"
            :mask="true"
            :styles="styles">
      <Form :model="formModel.fields" ref="formRole" :rules="formModel.rules" label-position="left">
        <Row :gutter="32">
          <Col span="12">
          <FormItem label="查询设备编号">
            <Select v-model="formModel.fields.deviceinformationindex"
                    filterable
                    clearable
                    remote
                    @on-change="handledeviceinformationkeyword"
                    :remote-method="handleLoaddeviceinformationDataSource"
                    :loading="stores.equipmentinstallation.sources.deviceinformationSources.loading"
                    placeholder="请选择设备编号...">
              <Option v-for="(item , deviceinformationindex) in stores.equipmentinstallation.sources.deviceinformationSources.data"
                      :value="deviceinformationindex"
                      :label="item.eunumber"
                      :key="deviceinformationindex">
                编号：{{ item.eunumber }}
              </Option>
            </Select>
          </FormItem>
          </Col>
          <Col span="12">
          <FormItem label="设备标识" prop="diid" label-position="left">
            <Input v-model="formModel.fields.diid" disabled placeholder="请输入产品型号" />
          </FormItem>
          </Col>
        </Row>
        <Row :gutter="32">

          <Col span="12">
          <FormItem label="查询缴费编号">
            <Select v-model="formModel.fields.installationpositionindex"
                    filterable
                    clearable
                    remote
                    @on-change="handleinstallationpositionkeyword"
                    :remote-method="handleLoadinstallationpositionDataSource"
                    :loading="stores.equipmentinstallation.sources.installationpositionSources.loading"
                    placeholder="请选择缴费编号...">
              <Option v-for="(item , installationpositionindex) in stores.equipmentinstallation.sources.installationpositionSources.data"
                      :value="installationpositionindex"
                      :label="item.payNumber"
                      :key="installationpositionindex">
                编号：{{ item.payNumber }}
              </Option>
            </Select>
          </FormItem>
          </Col>
          <Col span="12">
          <FormItem label="安装标识" prop="ipid" label-position="left">
            <Input v-model="formModel.fields.ipid" disabled placeholder="请输入程序版本" />
          </FormItem>
          </Col>
        </Row>
        <Row :gutter="32">
          <Col span="12">
          <FormItem label="安装时间" prop="installationTime" label-position="left">
            <DatePicker placeholder="请输入日期" v-model="formModel.fields.installationTime" />
          </FormItem>
          </Col>
          <Col span="10">
          <FormItem label="设备状态" label-position="left">
            <i-switch size="large" v-model="formModel.fields.status" :true-value="1" :false-value="0">
              <span slot="open">正常</span>
              <span slot="close">禁用</span>
            </i-switch>
          </FormItem>
          </Col>
        </Row>
        <Row :gutter="32">
          <Col span="12">
          <FormItem label="区域名称" label-position="left">
            <Input v-model="formModel.fields.areaName" disabled placeholder="请输入程序版本" />
          </FormItem>
          </Col>
          <Col span="12">
          <FormItem label="小区名称" label-position="left">
            <Input v-model="formModel.fields.uptownName" disabled placeholder="请输入程序版本" />
          </FormItem>
          </Col>
        </Row>
        <Row :gutter="32">
          <Col span="12">
          <FormItem label="楼幢名称" label-position="left">
            <Input v-model="formModel.fields.buildName" disabled placeholder="请输入程序版本" />
          </FormItem>
          </Col>
          <Col span="12">
          <FormItem label="单元名称" label-position="left">
            <Input v-model="formModel.fields.unitName" disabled placeholder="请输入程序版本" />
          </FormItem>
          </Col>
        </Row>
        <Row :gutter="32">
          <Col span="12">
          <FormItem label="地址" label-position="left">
            <Input v-model="formModel.fields.uptownAddr" disabled placeholder="请输入程序版本" />
          </FormItem>
          </Col>
         
        </Row>


        <FormItem label="备注" label-position="top">
          <Input type="textarea"
                 v-model="formModel.fields.remarks"
                 :rows="4"
                 placeholder="备注信息" />
        </FormItem>
      </Form>
      <div class="demo-drawer-footer">
        <Button icon="md-checkmark-circle" type="primary" @click="handleSubmitRole">保 存</Button>
        <Button style="margin-left: 8px" icon="md-close" @click="formModel.opened = false">取 消</Button>
      </div>
    </Drawer>
  </div>
</template>

<script>
  import Tables from "_c/tables";
  import {
    getRoleList,
    createRole,
    loadRole,
    editRole,
    deleteRole,
    batchCommand
  } from "@/api/rbac/equipmentInstallation";
  import { finddeviceInformationDataSourceByKeyword } from "@/api/rbac/deviceInformation";
  import { findinstallationpositionDataSourceByKeyword } from "@/api/rbac/installationPosition";
  export default {
    name: "rbac_equipmentinstallation_page",
    components: {
      Tables
    },
    data() {
      return {
        commands: {
          delete: { name: "delete", title: "删除" },
          recover: { name: "recover", title: "恢复" },
          forbidden: { name: "forbidden", title: "禁用" },
          normal: { name: "normal", title: "启用" }
        },
        formModel: {
          opened: false,
          title: "创建角色",
          mode: "create",
          selection: [],
          fields: {
            deviceinformationindex: 0,
            installationpositionindex:0,
            eiid: "",
            dateOfManufacture: "",
            diid: "1",
            eunumber: "",
            ipid: "",
            payNumber: "",
            installationTime: "2021-03-04T00:00:00",          
            areaId: "",
            areaName: "",
            uptownId: "",
            uptownName: "",
            uptownAddr: "",
            buildId: "",
            buildName: "",
            unitId: "",
            unitName: "",
            remarks: "",
            status: 1,
            isDeleted: 0
          },
          rules: {
            name: [
              {
                type: "string",
                required: true,
                message: "请输入角色名称",
                min: 2
              }
            ]
          }
        },
        stores: {
          equipmentinstallation: {
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
              ],
              deviceinformationSources: {
                loading: false,
                eunumber: "",
                data: []
              },
              installationpositionSources: {
                loading: false,
                eunumber: "",
                data: []
              }
            },
            columns: [
              { type: "selection", width: 50, key: "handle" },

              { title: "设备编号", key: "eunumber", width: 100, sortable: true },
              {
                title: "状态",
                key: "status",
                align: "center",
                width: 80,
                render: (h, params) => {
                  let status = params.row.status;
                  let statusColor = "success";
                  let statusText = "正常";
                  switch (status) {
                    case 0:
                      statusText = "禁用";
                      statusColor = "default";
                      break;
                  }
                  return h(
                    "Tooltip",
                    {
                      props: {
                        placement: "top",
                        transfer: true,
                        delay: 500
                      }
                    },
                    [
                      //这个中括号表示是Tooltip标签的子标签
                      h(
                        "Tag",
                        {
                          props: {
                            //type: "dot",
                            color: statusColor
                          }
                        },
                        statusText
                      ), //表格列显示文字
                      h(
                        "p",
                        {
                          slot: "content",
                          style: {
                            whiteSpace: "normal"
                          }
                        },
                        statusText //整个的信息即气泡内文字
                      )
                    ]
                  );
                }
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
              },
              { title: "设备标识", key: "diid", width: 100, sortable: true, ellipsis: true, tooltip: true },
              { title: "位置标识", key: "ipid", width: 100, sortable: true },
              { title: "缴费编号", key: "payNumber", width: 150, sortable: true, ellipsis: true, tooltip: true },
              { title: "安装时间", key: "installationTime", width: 100, sortable: true, ellipsis: true, tooltip: true },


              { title: "备注", key: "remarks", width: 200, sortable: true, ellipsis: true, tooltip: true }


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
      };
    },
    computed: {
      formTitle() {
        if (this.formModel.mode === "create") {
          return "创建角色";
        }
        if (this.formModel.mode === "edit") {
          return "编辑角色";
        }
        return "";
      },
      selectedRows() {
        return this.formModel.selection;
      },
      selectedRowsId() {
        return this.formModel.selection.map(x => x.eiid);
      }
    },
    methods: {
      loadRoleList() {
        getRoleList(this.stores.equipmentinstallation.query).then(res => {
          this.stores.equipmentinstallation.data = res.data.data;
          this.stores.equipmentinstallation.query.totalCount = res.data.totalCount;
        });
      },
      exportData() {
        this.$refs.tables.exportCsv({
          filename: "电子单元信息",
          original: false,
          columns: this.stores.equipmentinstallation.columns,
          data: this.stores.equipmentinstallation.data
        });
      },
      handleFormatError(file) {
        this.$Notice.warning({
          title: '文件格式不正确',
          desc: '文件 ' + file.name + ' 格式不正确，请上传.xls,.xlsx文件。'
        })
      },
      handleSuccess(res, file) {
        if (res.errcode === 0) {
          this.dialoLead = false
          this.$Message.success("数据导入成功！")
          this._getBookList()
          this.$refs.upload.clearFiles()
        }
      },
      handleError(error, file) {
        this.$Message.error("数据导入失败！")
      },
      handleOpenFormWindow() {
        this.formModel.opened = true;
      },
      handleCloseFormWindow() {
        this.formModel.opened = false;
      },
      handleSwitchFormModeToCreate() {
        this.formModel.mode = "create";
      },
      handleSwitchFormModeToEdit() {
        this.formModel.mode = "edit";
        this.handleOpenFormWindow();
      },
      handleEdit(params) {
        this.handleSwitchFormModeToEdit();
        this.handleResetFormRole();
        this.doLoadRole(params.row.eiid);
      },
      handleSelect(selection, row) { },
      handleSelectionChange(selection) {
        this.formModel.selection = selection;
      },
      handleRefresh() {
        this.loadRoleList();
      },
      handleShowCreateWindow() {
        this.handleSwitchFormModeToCreate();
        this.handleOpenFormWindow();
        this.handleResetFormRole();
      },
      handleSubmitRole() {
        let valid = this.validateRoleForm();
        if (valid) {
          if (this.formModel.mode === "create") {
            this.doCreateRole();
          }
          if (this.formModel.mode === "edit") {
            this.doEditRole();
          }
        }
      },
      handleResetFormRole() {
        this.$refs["formRole"].resetFields();
      },
      doCreateRole() {
        createRole(this.formModel.fields).then(res => {
          if (res.data.code === 200) {
            this.$Message.success(res.data.message);
            this.loadRoleList();
          } else {
            this.$Message.warning(res.data.message);
          }
          this.handleCloseFormWindow();
        });
      },
      doEditRole() {
        editRole(this.formModel.fields).then(res => {
          if (res.data.code === 200) {
            this.$Message.success(res.data.message);
            this.loadRoleList();
          } else {
            this.$Message.warning(res.data.message);
          }
          this.handleCloseFormWindow();
        });
      },
      validateRoleForm() {
        let _valid = false;
        this.$refs["formRole"].validate(valid => {
          if (!valid) {
            this.$Message.error("请完善表单信息");
            _valid = false;
          } else {
            _valid = true;
          }
        });
        return _valid;
      },
      doLoadRole(eiid) {
        loadRole({ eiid: eiid }).then(res => {
          this.formModel.fields = res.data.data;
        });
      },
      handleDelete(params) {
        this.doDelete(params.row.eiid);
      },
      doDelete(ids) {
        if (!ids) {
          this.$Message.warning("请选择至少一条数据");
          return;
        }
        deleteRole(ids).then(res => {
          if (res.data.code === 200) {
            this.$Message.success(res.data.message);
            this.loadRoleList();
          } else {
            this.$Message.warning(res.data.message);
          }
        });
      },
      handleBatchCommand(command) {
        if (!this.selectedRowsId || this.selectedRowsId.length <= 0) {
          this.$Message.warning("请选择至少一条数据");
          return;
        }
        this.$Modal.confirm({
          title: "操作提示",
          content:
            "<p>确定要执行当前 [" +
            this.commands[command].title +
            "] 操作吗?</p>",
          loading: true,
          onOk: () => {
            this.doBatchCommand(command);
          }
        });
      },
      doBatchCommand(command) {
        batchCommand({
          command: command,
          ids: this.selectedRowsId.join(",")
        }).then(res => {
          if (res.data.code === 200) {
            this.$Message.success(res.data.message);
            this.loadRoleList();
            this.formModel.selection = [];
          } else {
            this.$Message.warning(res.data.message);
          }
          this.$Modal.remove();
        });
      },
      handleSearchRole() {
        this.loadRoleList();
      },
      rowClsRender(row, index) {
        if (row.isDeleted) {
          return "table-row-disabled";
        }
        return "";
      },
      handlePageChanged(page) {
        this.stores.equipmentinstallation.query.currentPage = page;
        this.loadRoleList();
      },
      handlePageSizeChanged(pageSize) {
        this.stores.equipmentinstallation.query.pageSize = pageSize;
        this.loadRoleList();
      },
      handledeviceinformationkeyword() {
        this.formModel.fields.diid = this.stores.equipmentinstallation.sources.deviceinformationSources.data[this.formModel.fields.deviceinformationindex].diid;
        this.formModel.fields.eunumber = this.stores.equipmentinstallation.sources.deviceinformationSources.data[this.formModel.fields.deviceinformationindex].eunumber;

        //console.log(this.formModel.fields.ecuid)
      },
      handleLoaddeviceinformationDataSource(keyword) {
        this.stores.equipmentinstallation.sources.deviceinformationSources.loading = true;
        let query = { keyword: keyword };
        finddeviceInformationDataSourceByKeyword(query).then(res => {

          this.stores.equipmentinstallation.sources.deviceinformationSources.data = res.data.data;
          this.stores.equipmentinstallation.sources.deviceinformationSources.loading = false;

        });
      },
      handleinstallationpositionkeyword() {
        this.formModel.fields.ipid = this.stores.equipmentinstallation.sources.installationpositionSources.data[this.formModel.fields.installationpositionindex].ipid;
        this.formModel.fields.payNumber = this.stores.equipmentinstallation.sources.installationpositionSources.data[this.formModel.fields.installationpositionindex].payNumber;
        this.formModel.fields.areaName = this.stores.equipmentinstallation.sources.installationpositionSources.data[this.formModel.fields.installationpositionindex].areaName;
        this.formModel.fields.uptownName = this.stores.equipmentinstallation.sources.installationpositionSources.data[this.formModel.fields.installationpositionindex].uptownName;
        this.formModel.fields.buildName = this.stores.equipmentinstallation.sources.installationpositionSources.data[this.formModel.fields.installationpositionindex].buildName;
        this.formModel.fields.unitName = this.stores.equipmentinstallation.sources.installationpositionSources.data[this.formModel.fields.installationpositionindex].unitName;
        this.formModel.fields.uptownAddr = this.stores.equipmentinstallation.sources.installationpositionSources.data[this.formModel.fields.installationpositionindex].uptownAddr;

        //console.log(this.formModel.fields.ecuid)
      },
      handleLoadinstallationpositionDataSource(keyword) {
        this.stores.equipmentinstallation.sources.installationpositionSources.loading = true;
        let query = { keyword: keyword };
        findinstallationpositionDataSourceByKeyword(query).then(res => {

          this.stores.equipmentinstallation.sources.installationpositionSources.data = res.data.data;
          this.stores.equipmentinstallation.sources.installationpositionSources.loading = false;

        });
      }
    },
    mounted() {
      this.loadRoleList();
    }
  };</script>
               
