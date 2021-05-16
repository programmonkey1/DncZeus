<style>
.ivu-table .demo-table-info-row td {
  background-color: #2db7f5;
  color: #fff;
}
.ivu-table .demo-table-error-row td {
  background-color: #ff6600;
  color: #fff;
}
.ivu-table td.demo-table-info-column {
  background-color: #2db7f5;
  color: #fff;
}
.ivu-table .demo-table-info-cell-name {
  background-color: #2db7f5;
  color: #fff;
}
.ivu-table .demo-table-info-cell-age {
  background-color: #ff6600;
  color: #fff;
}
.ivu-table .demo-table-info-cell-address {
  background-color: #187;
  color: #fff;
}
</style>
<template>
  <div>
    <Card>
      <tables
        ref="tables"
        editable
        searchable
        :border="false"
        size="small"
        search-place="top"
        v-model="stores.worktask.data"
        :totalCount="stores.worktask.query.totalCount"
        :columns="stores.worktask.columns"
        @on-delete="handleDelete"
        @on-edit="handleEdit"
        @on-Submit-edit="handleSubmitEdit"
        @on-select="handleSelect"
        @on-selection-change="handleSelectionChange"
        @on-refresh="handleRefresh"
        :row-class-name="rowClsRender"
        @on-page-change="handlePageChanged"
        @on-page-size-change="handlePageSizeChanged"
      >
        <div slot="search">
          <section class="dnc-toolbar-wrap">
            <Row :gutter="16">
              <Col span="4">
                <Form inline @submit.native.prevent>
                  <FormItem>
                    <Input
                      type="text"
                      search
                      :clearable="true"
                      v-model="stores.worktask.query.kw"
                      placeholder="输入关键字搜索..."
                      @on-search="handleSearchRole()"
                    >
                      <Select
                        slot="prepend"
                        v-model="stores.worktask.query.isDeleted"
                        @on-change="handleSearchRole"
                        placeholder="删除状态"
                        style="width: 60px"
                      >
                        <Option
                          v-for="item in stores.worktask.sources
                            .isDeletedSources"
                          :value="item.value"
                          :key="item.value"
                        >
                          {{ item.text }}
                        </Option>
                      </Select>
                      <Select
                        slot="prepend"
                        v-model="stores.worktask.query.status"
                        @on-change="handleSearchRole"
                        placeholder="角色状态"
                        style="width: 60px"
                      >
                        <Option
                          v-for="item in stores.worktask.sources.statusSources"
                          :value="item.value"
                          :key="item.value"
                        >
                          {{ item.text }}
                        </Option>
                      </Select>
                    </Input>
                  </FormItem>
                </Form>
              </Col>
              <Col span="1">
                <Button
                  type="primary"
                  icon="md-create"
                  @click="exportData"
                  title="导出"
                >
                  导出
                </Button>
              </Col>
              <Col span="1">
                <Upload
                  ref="upload"
                  action="/api/book/excel/import"
                  name="excel-file"
                  :show-upload-list="true"
                  :on-format-error="handleFormatError"
                  :on-success="handleSuccess"
                  :on-error="handleError"
                  :format="['xlsx', 'xls']"
                >
                  <Button type="primary" icon="ios-cloud-upload-outline"
                    >批量导入</Button
                  >
                </Upload>
              </Col>
              <!-- <Col span="12" class="dnc-toolbar-btns">
              <Button
                    icon="md-create"
                    type="primary"
                    @click="handleShowCreateWindow"
                    title="新增主题"
                  >
                    新增主题
                  </Button>
              </Col> -->
              <Col span="18" class="dnc-toolbar-btns">
                <Button
                  icon="md-create"
                  type="primary"
                  @click="statistics = true"
                  title="统计"
                  >统计</Button
                >
                <Modal
                  v-model="statistics"
                  title="工作任务统计"
                  @on-ok="ok"
                  @on-cancel="cancel"
                >
                  <RadioGroup vertical v-model="statisticaldata">
                    <Radio label="提前完成"></Radio>
                    <Radio label="按期完成"></Radio>
                    <Radio label="延期完成"></Radio>
                    <Radio label="正在完成中"></Radio>
                    <Radio label="已逾期"></Radio>
                  </RadioGroup>
                </Modal>
                <ButtonGroup class="mr3">
                  <Button
                    class="txt-danger"
                    icon="md-trash"
                    title="删除"
                    @click="handleBatchCommand('delete')"
                  ></Button>
                  <Button
                    class="txt-success"
                    icon="md-redo"
                    title="恢复"
                    @click="handleBatchCommand('recover')"
                  ></Button>
                  <Button
                    class="txt-danger"
                    icon="md-hand"
                    title="禁用"
                    @click="handleBatchCommand('forbidden')"
                  ></Button>
                  <Button
                    class="txt-success"
                    icon="md-checkmark"
                    title="启用"
                    @click="handleBatchCommand('normal')"
                  ></Button>
                  <Button
                    icon="md-refresh"
                    title="刷新"
                    @click="handleRefresh"
                  ></Button>
                  <Button
                    icon="md-create"
                    type="primary"
                    @click="handleShowCreateWindow"
                    title="新增主题"
                  >
                    新增主题
                  </Button>
                </ButtonGroup>
              </Col>
            </Row>
          </section>
        </div>
      </tables>
    </Card>
    <Drawer
      :title="formTitle"
      v-model="formModel.opened"
      width="600"
      :mask-closable="true"
      :mask="true"
      :styles="styles"
    >
      <Form
        :model="formModel.fields"
        ref="formRole"
        :rules="formModel.rules"
        label-position="left"
      >
        <FormItem label="任务主题" label-position="top">
          <Input
            v-model="formModel.fields.taskTheme"
            placeholder="请输入任务主题"
          />
        </FormItem>
        <FormItem label="任务内容" label-position="top">
          <Input
            type="textarea"
            v-model="formModel.fields.taskContent"
            :rows="2"
            placeholder="请输入任务内容"
          />
        </FormItem>
        <Row :gutter="32">
          <Col span="12">
            <FormItem label="任务重要程度">
              <Select v-model="formModel.fields.workType">
                <Option
                  v-for="item in workTypeList"
                  :value="item.value"
                  :key="item.value"
                  >{{ item.label }}
                </Option>
              </Select>
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="完成时间节点">
              <DatePicker
                v-model="formModel.fields.completionFirstTime"
                format="yyyy-MM-dd"
                type="date"
                confirm
                placement="bottom-end"
                placeholder="清选择开始时间"
                @on-change="getnowTime"
              ></DatePicker>
              <DatePicker
                v-model="formModel.fields.completionendTime"
                format="yyyy-MM-dd"
                type="date"
                confirm
                placement="bottom-end"
                placeholder="清选择结束时间"
                @on-change="getnowTime"
              ></DatePicker>
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="32">
          <Col span="12">
            <FormItem label="任务人" label-position="top">
              <Input
                v-model="formModel.fields.taskPerson"
                placeholder="请输入任务人"
              />
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="联系电话" label-position="top">
              <Input
                v-model="formModel.fields.telephone"
                placeholder="请输入联系电话"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="32">
          <Col span="12">
            <FormItem label="项目经理" label-position="top">
              <Input
                v-model="formModel.fields.projectManager"
                placeholder="请输入项目经理"
              />
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="发布人" label-position="top">
              <Input
                v-model="formModel.fields.publisher"
                placeholder="请输入发布人"
              />
            </FormItem>
          </Col>
        </Row>
        <FormItem label="第三方配合事项" label-position="top">
          <Input
            type="textarea"
            v-model="formModel.fields.thirdPartyCooperation"
            :rows="2"
            placeholder="请输入第三方配合内容"
          />
        </FormItem>
        <FormItem label="注意事项" label-position="top">
          <Input
            type="textarea"
            v-model="formModel.fields.mattersNeedingAttention"
            :rows="2"
            placeholder="请输入注意事项"
          />
        </FormItem>
      </Form>
      <div class="demo-drawer-footer">
        <Button
          icon="md-checkmark-circle"
          type="primary"
          @click="handleSubmitRole"
          >提 交
        </Button>
        <Button
          style="margin-left: 8px"
          icon="md-close"
          @click="formSubmitModel.opened = false"
          >取 消
        </Button>
      </div>
    </Drawer>
    <Drawer
      :title="formSubmit"
      v-model="formSubmitModel.opened"
      width="600"
      :mask-closable="true"
      :mask="true"
      :styles="styles"
    >
      <Form
        :model="formSubmitModel.fields"
        ref="formSubmit"
        :rules="formSubmitModel.rules"
        label-position="left"
      >
        <FormItem label="任务主题" label-position="top">
          <Input
            v-model="formSubmitModel.fields.taskTheme"
            placeholder="请输入任务主题"
            disabled
          />
        </FormItem>
        <FormItem label="任务内容" label-position="top">
          <Input
            type="textarea"
            v-model="formSubmitModel.fields.taskContent"
            :rows="2"
            placeholder="请输入任务内容"
            disabled
          />
        </FormItem>
        <Row :gutter="32">
          <Col span="12">
            <FormItem label="任务重要程度">
              <Select v-model="formSubmitModel.fields.workType" disabled>
                <Option
                  v-for="item in workTypeList"
                  :value="item.value"
                  :key="item.value"
                  >{{ item.label }}
                </Option>
              </Select>
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="完成时间节点">
              <DatePicker
                v-model="formSubmitModel.fields.completionFirstTime"
                format="yyyy-MM-dd"
                type="date"
                confirm
                disabled
                placement="bottom-end"
                placeholder="清选择开始时间"
                @on-change="getnowTime"
              ></DatePicker>
              <DatePicker
                v-model="formSubmitModel.fields.completionEndTime"
                format="yyyy-MM-dd"
                type="date"
                confirm
                disabled
                placement="bottom-end"
                placeholder="清选择结束时间"
                @on-change="getnowTime"
              ></DatePicker>
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="32">
          <Col span="12">
            <FormItem label="任务人" label-position="top">
              <Input
                v-model="formSubmitModel.fields.taskPerson"
                placeholder="请输入任务人"
                disabled
              />
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="联系电话" label-position="top">
              <Input
                v-model="formSubmitModel.fields.telephone"
                placeholder="请输入联系电话"
                disabled
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="32">
          <Col span="12">
            <FormItem label="项目经理" label-position="top">
              <Input
                v-model="formSubmitModel.fields.projectManager"
                placeholder="请输入项目经理"
                disabled
              />
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="发布人" label-position="top">
              <Input
                v-model="formSubmitModel.fields.publisher"
                placeholder="请输入发布人"
                disabled
              />
            </FormItem>
          </Col>
        </Row>
        <FormItem label="第三方配合事项" label-position="top">
          <Input
            type="textarea"
            v-model="formSubmitModel.fields.thirdPartyCooperation"
            :rows="4"
            placeholder="请输入第三方配合内容"
            disabled
          />
        </FormItem>
        <FormItem label="注意事项" label-position="top">
          <Input
            type="textarea"
            v-model="formSubmitModel.fields.mattersNeedingAttention"
            :rows="4"
            placeholder="请输入注意事项"
            disabled
          />
        </FormItem>
        <RadioGroup
          v-model="formSubmitModel.fields.progressDeviation"
          v-if="formSubmitModel.fields.progressDeviation === '正在完成中'"
        >
          <Radio label="提前完成"></Radio>
          <Radio label="按期完成" disabled></Radio>
          <Radio label="延期完成" disabled></Radio>
        </RadioGroup>
        <RadioGroup
          v-model="formSubmitModel.fields.progressDeviation"
          v-if="formSubmitModel.fields.progressDeviation === '任务最后一天'"
        >
          <Radio label="提前完成" disabled></Radio>
          <Radio label="按期完成"></Radio>
          <Radio label="延期完成" disabled></Radio>
        </RadioGroup>
        <RadioGroup
          v-model="formSubmitModel.fields.progressDeviation"
          v-if="formSubmitModel.fields.progressDeviation === '已逾期'"
        >
          <Radio label="提前完成" disabled></Radio>
          <Radio label="按期完成" disabled></Radio>
          <Radio label="延期完成"></Radio>
        </RadioGroup>
        <RadioGroup
          v-model="formSubmitModel.fields.progressDeviation"
          v-if="
            formSubmitModel.fields.progressDeviation === '提前完成' ||
            formSubmitModel.fields.progressDeviation === '按期完成' ||
            formSubmitModel.fields.progressDeviation === '延期完成'
          "
        >
          <Radio label="提前完成" disabled></Radio>
          <Radio label="按期完成" disabled></Radio>
          <Radio label="延期完成" disabled></Radio>
        </RadioGroup>

        <FormItem label="完成情况说明" label-position="top">
          <Input
            type="textarea"
            v-model="formSubmitModel.fields.informationNote"
            :rows="4"
            placeholder="请输入情况说明"
          />
        </FormItem>
        <FormItem label="新增第三方配合事项" label-position="top">
          <div id="div">
            <Input
              type="textarea"
              v-model="formSubmitModel.fields.addthirdPartyCooperation"
              :rows="4"
              placeholder="请输入第三方配合事项"
            />
          </div>
        </FormItem>
        <FormItem label="新增注意事项" label-position="top">
          <Input
            type="textarea"
            v-model="formSubmitModel.fields.addmattersNeedingAttention"
            :rows="4"
            placeholder="请输入注意事项"
          />
        </FormItem>
      </Form>
      <div class="demo-drawer-footer">
        <Button
          icon="md-checkmark-circle"
          type="primary"
          @click="handleeditsubmit"
          >完 成</Button
        >
        <Button
          style="margin-left: 8px"
          icon="md-close"
          @click="formSubmitModel.opened = false"
          >取 消</Button
        >
      </div>
    </Drawer>
  </div>
</template>

<script>
import Tables from "_c/tables";
import {
  getWorkTaskList,
  createWorkTask,
  loadWorkTask,
  editWorkTask,
  deleteWorkTask,
  batchCommand,
  gettimeList,
} from "@/api/rbac/worktask";
import { findEcutableDataSourceByKeyword } from "@/api/rbac/ECUTable";
import { findbasetableDataSourceByKeyword } from "@/api/rbac/basetable";
import backBtnGroupVue from "../error-page/back-btn-group.vue";

export default {
  name: "rbac_worktask_page",
  components: {
    Tables,
  },
  data() {
    return {
      statisticaldata:"",
      statistics: false,
      workTypeList: [
        {
          value: 0,
          label: "重要工作",
        },
        {
          value: 1,
          label: "一般工作",
        },
        {
          value: 2,
          label: "次要工作",
        },
      ],

      commands: {
        delete: { name: "delete", title: "删除" },
        recover: { name: "recover", title: "恢复" },
        forbidden: { name: "forbidden", title: "禁用" },
        normal: { name: "normal", title: "启用" },
      },
      formModel: {
        opened: false,
        title: "创建",
        mode: "create",
        valid: true,
        selection: [],
        selectOption: {
          ecutable: {},
        },
        //model内容
        fields: {
          //主题
          taskTheme: "",
          //任务内容
          taskContent: "",
          //任务类型
          workType: "",
          //完成时间节点
          completionFirstTime: "",
          //完成时间节点-开始
          completionEndTime: "",
          //完成时间节点-结束
          completionTime: "",
          //任务人
          taskPerson: "",
          //联系电话
          telephone: "",
          taskplan: "",
          planlist: "",
          informationCode: "",
          //项目经理
          projectManager: "",
          //发布人
          publisher: "",
          //添加第三方配合事项
          addthirdPartyCooperation: "",
          //第三方配合事项
          thirdPartyCooperation: "",
          //添加注意事项
          addmattersNeedingAttention: "",
          //注意事项
          mattersNeedingAttention: "",
          // no1: 0,
          // no2: 0,
          // no3: 0,
          // no4: 0,
          // no5: 0,
          // no6: 0,
          // no7: 0,
          // no8: 0,
          // no9: 0,
          // no10: 0,
          // no11: 0,
          // no12: 0,
          // no13: 0,
          // no14: 0,
          // no15: 0,
          // no16: 0,
          // no17: 0,
          // no18: 0,
          // no19: 0,
          // no20: 0,
          // no21: 0,
          // no22: 0,
          // no23: 0,
          // no24: 0,
          // no25: 0,
          // no26: 0,
          // no27: 0,
          // no28: 0,
          // no29: 0,
          // no30: 0,
          // no31: 0,
          progressDeviation: "",
          status: 0,
          isDeleted: 0,
          isFinished: 0,
          code: "",
        },
        rules: {
          name: [
            {
              type: "string",
              required: true,
              message: "请输入角色名称",
              min: 3,
            },
          ],
        },
      },
      formSubmitModel: {
        opened: false,
        title: "编辑",
        mode: "edit",
        selection: [],
        selectOption: {
          ecutable: {},
        },
        fields: {
          id: "",
          progressDeviation: "",
          // ttaskOffice: '',
          //添加第三方配合事项
          addthirdPartyCooperation: "",
          //第三方配合事项
          thirdPartyCooperation: "",
          //添加注意事项
          addmattersNeedingAttention: "",
          //注意事项
          mattersNeedingAttention: "",

          status: 0,
          isDeleted: 0,
          isFinished: 0,
          code: "",
        },
        rules: {
          name: [
            {
              type: "string",
              required: true,
              message: "请输入角色名称",
              min: 2,
            },
          ],
        },
      },
      stores: {
        worktask: {
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
                field: "CreatedOn",
              },
            ],
          },
          sources: {
            isDeletedSources: [
              { value: -1, text: "全部" },
              { value: 0, text: "正常" },
              { value: 1, text: "已删" },
            ],
            statusSources: [
              { value: -1, text: "全部" },
              { value: 0, text: "禁用" },
              { value: 1, text: "正常" },
            ],
            statusFormSources: [
              { value: 0, text: "禁用" },
              { value: 1, text: "正常" },
            ],
            ecutableSources: {
              loading: false,
              electronicUnitNumber: "",
              data: [],
            },
            basetableSources: {
              loading: false,
              electronicUnitNumber: "",
              data: [],
            },
          },
          columns: [
            { type: "selection", width: 50, key: "handle" },

            // {
            //   title: "序号",
            //   key: "id",
            //   width: 50,
            //   sortable: true,
            //   ellipsis: true,
            //   tooltip: true,
            // },
            {
              title: "主题",
              key: "taskTheme",
              width: 150,
              sortable: true,
              ellipsis: true,
              tooltip: true,
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
                        title: "你确定要删除吗?",
                        placement: "right",
                      },
                      on: {
                        "on-ok": () => {
                          vm.$emit("on-delete", params);
                        },
                      },
                    },
                    [
                      h(
                        "Tooltip",
                        {
                          props: {
                            placement: "left",
                            transfer: true,
                            delay: 1000,
                          },
                        },
                        [
                          h("Button", {
                            props: {
                              shape: "circle",
                              size: "small",
                              icon: "md-trash",
                              type: "error",
                            },
                          }),
                          h(
                            "p",
                            {
                              slot: "content",
                              style: {
                                whiteSpace: "normal",
                              },
                            },
                            "删除"
                          ),
                        ]
                      ),
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
                        delay: 1000,
                      },
                    },
                    [
                      h("Button", {
                        props: {
                          shape: "circle",
                          size: "small",
                          icon: "md-create",
                          type: "primary",
                        },
                        on: {
                          click: () => {
                            vm.$emit("on-Submit-edit", params);
                            vm.$emit("input", params.tableData);
                          },
                        },
                      }),
                      h(
                        "p",
                        {
                          slot: "content",
                          style: {
                            whiteSpace: "normal",
                          },
                        },
                        "提交"
                      ),
                    ]
                  );
                },
              ],
            },
            {
              title: "任务内容",
              key: "taskContent",
              width: 200,
              sortable: true,
              ellipsis: true,
              tooltip: true,
            },
            {
              title: "任务类型",
              key: "workType",
              width: 100,
              sortable: true,
              render: (h, params) => {
                let workType = params.row.workType;
                if (workType == 0) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "13px",
                        padding: "5px 10px",
                        cursor: "pointer",
                        color: "#FF0000",
                      },
                    },
                    "重要工作"
                  );
                } else if (workType == 1) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "13px",
                        padding: "5px 10px",
                        cursor: "pointer",
                        color: "#FFFF00",
                      },
                    },
                    "一般工作"
                  );
                } else if (workType == 2) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "13px",
                        padding: "5px 10px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "次要工作"
                  );
                }
              },
            },
            {
              title: "月份",
              key: "taskPlan",
              width: 50,
              sortable: true,
              ellipsis: true,
              tooltip: true,
            },
            {
              title: "1",
              key: "no1",
              width: 30,
              ellipsis: true,
              tooltip: true,
              render: (h, params) => {
                let no1 = params.row.no1;
                if (no1 == 0) {
                  return h("span", {
                    style: {},
                  });
                } else if (no1 == 1) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟩"
                  );
                } else if (no1 == 2) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟥"
                  );
                } else if (no1 == 3) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟦"
                  );
                }
              },
            },
            {
              title: "2",
              key: "no2",
              width: 30,
              ellipsis: true,
              tooltip: true,
              render: (h, params) => {
                let no2 = params.row.no2;
                if (no2 == 0) {
                  return h("span", {
                    style: {},
                  });
                } else if (no2 == 1) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟩"
                  );
                } else if (no2 == 2) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟥"
                  );
                } else if (no2 == 3) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟦"
                  );
                }
              },
            },
            {
              title: "3",
              key: "no3",
              width: 30,
              ellipsis: true,
              tooltip: true,
              render: (h, params) => {
                let no3 = params.row.no3;
                if (no3 == 0) {
                  return h("span", {
                    style: {},
                  });
                } else if (no3 == 1) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟩"
                  );
                } else if (no3 == 2) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟥"
                  );
                } else if (no3 == 3) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟦"
                  );
                }
              },
            },
            {
              title: "4",
              key: "no4",
              width: 30,
              ellipsis: true,
              tooltip: true,
              render: (h, params) => {
                let no4 = params.row.no4;
                if (no4 == 0) {
                  return h("span", {
                    style: {},
                  });
                } else if (no4 == 1) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟩"
                  );
                } else if (no4 == 2) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟥"
                  );
                } else if (no4 == 3) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟦"
                  );
                }
              },
            },
            {
              title: "5",
              key: "no5",
              width: 30,
              ellipsis: true,
              tooltip: true,
              render: (h, params) => {
                let no5 = params.row.no5;
                if (no5 == 0) {
                  return h("span", {
                    style: {},
                  });
                } else if (no5 == 1) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟩"
                  );
                } else if (no5 == 2) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟥"
                  );
                } else if (no5 == 3) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟦"
                  );
                }
              },
            },
            {
              title: "6",
              key: "no6",
              width: 30,
              ellipsis: true,
              tooltip: true,
              render: (h, params) => {
                let no6 = params.row.no6;
                if (no6 == 0) {
                  return h("span", {
                    style: {},
                  });
                } else if (no6 == 1) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟩"
                  );
                } else if (no6 == 2) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟥"
                  );
                } else if (no6 == 3) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟦"
                  );
                }
              },
            },
            {
              title: "7",
              key: "no7",
              width: 30,
              ellipsis: true,
              tooltip: true,
              render: (h, params) => {
                let no7 = params.row.no7;
                if (no7 == 0) {
                  return h("span", {
                    style: {},
                  });
                } else if (no7 == 1) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟩"
                  );
                } else if (no7 == 2) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟥"
                  );
                } else if (no7 == 3) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟦"
                  );
                }
              },
            },
            {
              title: "8",
              key: "no8",
              width: 30,
              ellipsis: true,
              tooltip: true,
              render: (h, params) => {
                let no8 = params.row.no8;
                if (no8 == 0) {
                  return h("span", {
                    style: {},
                  });
                } else if (no8 == 1) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟩"
                  );
                } else if (no8 == 2) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟥"
                  );
                } else if (no8 == 3) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟦"
                  );
                }
              },
            },
            {
              title: "9",
              key: "no9",
              width: 30,
              ellipsis: true,
              tooltip: true,
              render: (h, params) => {
                let no9 = params.row.no9;
                if (no9 == 0) {
                  return h("span", {
                    style: {},
                  });
                } else if (no9 == 1) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟩"
                  );
                } else if (no9 == 2) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟥"
                  );
                } else if (no9 == 3) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟦"
                  );
                }
              },
            },
            {
              title: "10",
              key: "no10",
              width: 30,
              ellipsis: true,
              tooltip: true,
              render: (h, params) => {
                let no10 = params.row.no10;
                if (no10 == 0) {
                  return h("span", {
                    style: {},
                  });
                } else if (no10 == 1) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟩"
                  );
                } else if (no10 == 2) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟥"
                  );
                } else if (no10 == 3) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟦"
                  );
                }
              },
            },
            {
              title: "11",
              key: "no11",
              width: 30,
              ellipsis: true,
              tooltip: true,
              render: (h, params) => {
                let no11 = params.row.no11;
                if (no11 == 0) {
                  return h("span", {
                    style: {},
                  });
                } else if (no11 == 1) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟩"
                  );
                } else if (no11 == 2) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟥"
                  );
                } else if (no11 == 3) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟦"
                  );
                }
              },
            },
            {
              title: "12",
              key: "no12",
              width: 30,
              ellipsis: true,
              tooltip: true,
              render: (h, params) => {
                let no12 = params.row.no12;
                if (no12 == 0) {
                  return h("span", {
                    style: {},
                  });
                } else if (no12 == 1) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟩"
                  );
                } else if (no12 == 2) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟥"
                  );
                } else if (no12 == 3) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟦"
                  );
                }
              },
            },
            {
              title: "13",
              key: "no13",
              width: 30,
              ellipsis: true,
              tooltip: true,
              render: (h, params) => {
                let no13 = params.row.no13;
                if (no13 == 0) {
                  return h("span", {
                    style: {},
                  });
                } else if (no13 == 1) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟩"
                  );
                } else if (no13 == 2) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟥"
                  );
                } else if (no13 == 3) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟦"
                  );
                }
              },
            },
            {
              title: "14",
              key: "no14",
              width: 30,
              ellipsis: true,
              tooltip: true,
              render: (h, params) => {
                let no14 = params.row.no14;
                if (no14 == 0) {
                  return h("span", {
                    style: {},
                  });
                } else if (no14 == 1) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟩"
                  );
                } else if (no14 == 2) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟥"
                  );
                } else if (no14 == 3) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟦"
                  );
                }
              },
            },
            {
              title: "15",
              key: "no15",
              width: 30,
              ellipsis: true,
              tooltip: true,
              render: (h, params) => {
                let no15 = params.row.no15;
                if (no15 == 0) {
                  return h("span", {
                    style: {},
                  });
                } else if (no15 == 1) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟩"
                  );
                } else if (no15 == 2) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟥"
                  );
                } else if (no15 == 3) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟦"
                  );
                }
              },
            },
            {
              title: "16",
              key: "no16",
              width: 30,
              ellipsis: true,
              tooltip: true,
              render: (h, params) => {
                let no16 = params.row.no16;
                if (no16 == 0) {
                  return h("span", {
                    style: {},
                  });
                } else if (no16 == 1) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟩"
                  );
                } else if (no16 == 2) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟥"
                  );
                } else if (no16 == 3) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟦"
                  );
                }
              },
            },
            {
              title: "17",
              key: "no17",
              width: 30,
              ellipsis: true,
              tooltip: true,
              render: (h, params) => {
                let no17 = params.row.no17;
                if (no17 == 0) {
                  return h("span", {
                    style: {},
                  });
                } else if (no17 == 1) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟩"
                  );
                } else if (no17 == 2) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟥"
                  );
                } else if (no17 == 3) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟦"
                  );
                }
              },
            },
            {
              title: "18",
              key: "no18",
              width: 30,
              ellipsis: true,
              tooltip: true,
              render: (h, params) => {
                let no18 = params.row.no18;
                if (no18 == 0) {
                  return h("span", {
                    style: {},
                  });
                } else if (no18 == 1) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟩"
                  );
                } else if (no18 == 2) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟥"
                  );
                } else if (no18 == 3) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟦"
                  );
                }
              },
            },
            {
              title: "19",
              key: "no19",
              width: 30,
              ellipsis: true,
              tooltip: true,
              render: (h, params) => {
                let no19 = params.row.no19;
                if (no19 == 0) {
                  return h("span", {
                    style: {},
                  });
                } else if (no19 == 1) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟩"
                  );
                } else if (no19 == 2) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟥"
                  );
                } else if (no19 == 3) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟦"
                  );
                }
              },
            },
            {
              title: "20",
              key: "no20",
              width: 30,
              ellipsis: true,
              tooltip: true,
              render: (h, params) => {
                let no20 = params.row.no20;
                if (no20 == 0) {
                  return h("span", {
                    style: {},
                  });
                } else if (no20 == 1) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟩"
                  );
                } else if (no20 == 2) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟥"
                  );
                } else if (no20 == 3) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟦"
                  );
                }
              },
            },
            {
              title: "21",
              key: "no21",
              width: 30,
              ellipsis: true,
              tooltip: true,
              render: (h, params) => {
                let no21 = params.row.no21;
                if (no21 == 0) {
                  return h("span", {
                    style: {},
                  });
                } else if (no21 == 1) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟩"
                  );
                } else if (no21 == 2) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟥"
                  );
                } else if (no21 == 3) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟦"
                  );
                }
              },
            },
            {
              title: "22",
              key: "no22",
              width: 30,
              ellipsis: true,
              tooltip: true,
              render: (h, params) => {
                let no22 = params.row.no22;
                if (no22 == 0) {
                  return h("span", {
                    style: {},
                  });
                } else if (no22 == 1) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟩"
                  );
                } else if (no22 == 2) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟥"
                  );
                } else if (no22 == 3) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟦"
                  );
                }
              },
            },
            {
              title: "23",
              key: "no23",
              width: 30,
              ellipsis: true,
              tooltip: true,
              render: (h, params) => {
                let no23 = params.row.no23;
                if (no23 == 0) {
                  return h("span", {
                    style: {},
                  });
                } else if (no23 == 1) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟩"
                  );
                } else if (no23 == 2) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟥"
                  );
                } else if (no23 == 3) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟦"
                  );
                }
              },
            },
            {
              title: "24",
              key: "no24",
              width: 30,
              ellipsis: true,
              tooltip: true,
              render: (h, params) => {
                let no24 = params.row.no24;
                if (no24 == 0) {
                  return h("span", {
                    style: {},
                  });
                } else if (no24 == 1) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟩"
                  );
                } else if (no24 == 2) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟥"
                  );
                } else if (no24 == 3) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟦"
                  );
                }
              },
            },
            {
              title: "25",
              key: "no25",
              width: 30,
              ellipsis: true,
              tooltip: true,
              render: (h, params) => {
                let no25 = params.row.no25;
                if (no25 == 0) {
                  return h("span", {
                    style: {},
                  });
                } else if (no25 == 1) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟩"
                  );
                } else if (no25 == 2) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟥"
                  );
                } else if (no25 == 3) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟦"
                  );
                }
              },
            },
            {
              title: "26",
              key: "no26",
              width: 30,
              ellipsis: true,
              tooltip: true,
              render: (h, params) => {
                let no26 = params.row.no26;
                if (no26 == 0) {
                  return h("span", {
                    style: {},
                  });
                } else if (no26 == 1) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟩"
                  );
                } else if (no26 == 2) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟥"
                  );
                } else if (no26 == 3) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟦"
                  );
                }
              },
            },
            {
              title: "27",
              key: "no27",
              width: 30,
              ellipsis: true,
              tooltip: true,
              render: (h, params) => {
                let no27 = params.row.no27;
                if (no27 == 0) {
                  return h("span", {
                    style: {},
                  });
                } else if (no27 == 1) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟩"
                  );
                } else if (no27 == 2) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟥"
                  );
                } else if (no27 == 3) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟦"
                  );
                }
              },
            },
            {
              title: "28",
              key: "no28",
              width: 30,
              ellipsis: true,
              tooltip: true,
              render: (h, params) => {
                let no28 = params.row.no28;
                if (no28 == 0) {
                  return h("span", {
                    style: {},
                  });
                } else if (no28 == 1) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟩"
                  );
                } else if (no28 == 2) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟥"
                  );
                } else if (no28 == 3) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟦"
                  );
                }
              },
            },
            {
              title: "29",
              key: "no29",
              width: 30,
              ellipsis: true,
              tooltip: true,
              render: (h, params) => {
                let no29 = params.row.no29;
                if (no29 == 0) {
                  return h("span", {
                    style: {},
                  });
                } else if (no29 == 1) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟩"
                  );
                } else if (no29 == 2) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟥"
                  );
                } else if (no29 == 3) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟦"
                  );
                }
              },
            },
            {
              title: "30",
              key: "no30",
              width: 30,
              ellipsis: true,
              tooltip: true,
              render: (h, params) => {
                let no30 = params.row.no30;
                if (no30 == 0) {
                  return h("span", {
                    style: {},
                  });
                } else if (no30 == 1) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟩"
                  );
                } else if (no30 == 2) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟥"
                  );
                } else if (no30 == 3) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟦"
                  );
                }
              },
            },
            {
              title: "31",
              key: "no31",
              width: 30,
              ellipsis: true,
              tooltip: true,
              render: (h, params) => {
                let no31 = params.row.no31;
                if (no31 == 0) {
                  return h("span", {
                    style: {},
                  });
                } else if (no31 == 1) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟩"
                  );
                } else if (no31 == 2) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟥"
                  );
                } else if (no31 == 3) {
                  return h(
                    "span",
                    {
                      style: {
                        fontSize: "15px",
                        padding: "0px 0px",
                        cursor: "pointer",
                        color: "#00FF00",
                      },
                    },
                    "🟦"
                  );
                }
              },
            },
            {
              title: "任务人",
              key: "taskPerson",
              width: 80,
              sortable: true,
              ellipsis: true,
              tooltip: true,
            },
            {
              title: "联系电话",
              key: "telephone",
              width: 100,
              ellipsis: true,
              tooltip: true,
            },
            {
              title: "任务时间",
              key: "taskTime",
              width: 150,
              sortable: true,
              ellipsis: true,
              tooltip: true,
            },
            {
              title: "完成时间节点",
              key: "completionTime",
              width: 180,
              ellipsis: true,
              tooltip: true,
            },

            {
              title: "进度偏离",
              key: "progressDeviation",
              width: 100,
              ellipsis: true,
              tooltip: true,
            },
            {
              title: "情况说明",
              key: "informationNote",
              width: 150,
              ellipsis: true,
              tooltip: true,
            },
            {
              title: "第三方配合事项",
              key: "thirdPartyCooperation",
              width: 150,
              ellipsis: true,
              tooltip: true,
            },
            {
              title: "注意事项",
              key: "mattersNeedingAttention",
              width: 150,
              ellipsis: true,
              tooltip: true,
            },
            {
              title: "项目经理",
              key: "projectManager",
              width: 100,
              ellipsis: true,
              tooltip: true,
            },
            {
              title: "发布人",
              key: "publisher",
              width: 100,
              ellipsis: true,
              tooltip: true,
            },
          ],
          data: [],
        },
      },
      styles: {
        height: "calc(100% - 55px)",
        overflow: "auto",
        paddingBottom: "53px",
        position: "static",
      },
    };
  },
  computed: {
    formTitle() {
      if (this.formModel.mode === "create") {
        return "创建主题";
      }
      if (this.formModel.mode === "edit") {
        return "编辑主题";
      }
      return "";
    },

    selectedRows() {
      return this.formModel.selection;
    },
    selectedRowsId() {
      return this.formModel.selection.map((x) => x.id);
    },
  },
  methods: {
    ok() {
      this.loadkwList();
    },
    cancel() {},
    loadkwList() {
      findworktaskDataSourceByprogressdeviation(this.statisticaldata).then((res) => {
        this.stores.worktask.data = res.data.data;
        console.log(res.data.data);
        this.stores.worktask.query.totalCount = res.data.totalCount;
      });
    },
    loadRoleList() {
      getWorkTaskList(this.stores.worktask.query).then((res) => {
        this.stores.worktask.data = res.data.data;
        console.log(res.data.data);
        this.stores.worktask.query.totalCount = res.data.totalCount;
      });
    },
    loadtimeList() {
      gettimeList(this.stores.worktask.query).then((res) => {
        this.stores.worktask.data = res.data.data;
        console.log(res.data.data);
        this.stores.worktask.query.totalCount = res.data.totalCount;
      });
    },
    exportData() {
      this.$refs.tables.exportCsv({
        filename: "电子单元信息",
        original: false,
        columns: this.stores.worktask.columns,
        data: this.stores.worktask.data,
      });
    },
    handleFormatError(file) {
      this.$Notice.warning({
        title: "文件格式不正确",
        desc: "文件 " + file.nasme + " 格式不正确，请上传.xls,.xlsx文件。",
      });
    },
    handleSuccess(res, file) {
      if (res.errcode === 0) {
        this.dialoLead = false;
        this.$Message.success("数据导入成功！");
        this._getBookList();
        this.$refs.upload.clearFiles();
      }
    },
    handleError(error, file) {
      this.$Message.error("数据导入失败！");
    },
    handleOpenFormWindow() {
      this.formModel.opened = true;
    },
    handleOpenFormWindowSubmit() {
      this.formSubmitModel.opened = true;
    },
    //关闭抽屉
    handleCloseFormWindow() {
      this.formModel.opened = false;
    },
    handleCloseSubmitFormWindow() {
      this.formSubmitModel.opened = false;
    },
    handleSwitchFormModeToCreate() {
      this.formModel.mode = "create";
    },
    handleSwitchFormModeToCreateSubmit() {
      this.formSubmitModel.mode = "create";
    },
    handleSwitchFormModeToEdit() {
      this.formModel.mode = "edit";
      this.handleOpenFormWindow();
    },
    handleSwitchFormSubmitModeToEdit() {
      this.formSubmitModel.mode = "edit";
      this.handleOpenFormWindowSubmit();
    },
    handleEdit(params) {
      this.handleSwitchFormModeToEdit();
      this.handleResetFormRole();
      this.doLoadRole(params.row.id);
    },
    handleSubmitEdit(params) {
      this.handleSwitchFormSubmitModeToEdit();
      this.handleResetFormSubmit();
      this.doLoadsubmit(params.row.code);
    },
    handleSelect(selection, row) {},
    handleSelectionChange(selection) {
      this.formModel.selection = selection;
    },
    handleRefresh() {
      this.loadRoleList();
    },
    //新建主题点击事件的方法
    handleShowCreateWindow() {
      this.handleSwitchFormModeToCreate();
      this.handleOpenFormWindow();
      this.handleResetFormRole();
      //主题
      this.formModel.fields.taskTheme = "";
      //任务内容
      this.formModel.fields.taskContent = "";
      //任务类型
      this.formModel.fields.workType = "";
      //完成时间节点
      this.formModel.fields.completionFirstTime = "";
      //完成时间节点-开始
      this.formModel.fields.completionEndTime = "";
      //完成时间节点-结束
      this.formModel.fields.completionTime = "";
      //任务人
      this.formModel.fields.taskPerson = "";
      //联系电话
      this.formModel.fields.telephone = "";
      this.formModel.fields.taskplan = "";
      this.formModel.fields.planlist = "";
      this.formModel.fields.informationCode = "";
      //项目经理
      this.formModel.fields.projectManager = "";
      //发布人
      this.formModel.fields.publisher = "";
      //第三方配合事项
      this.formModel.fields.thirdPartyCooperation = "";
      //注意事项
      this.formModel.fields.mattersNeedingAttention = "";
    },
    // handleShowSubmitWindow() {
    //   this.handleSwitchFormModeToCreateSubmit();
    //   this.handleOpenFormWindowSubmit();
    //   this.handleResetFormSubmit();
    // },

    handleSaveRolePermissions() {
      var data = {
        roleCode: this.currentRoleCode,
        permissions: this.selectedPermissions,
      };
      assignPermission(data).then((response) => {
        var result = response.data;
        if (result.code == 200) {
          this.$Message.success(result.message);
        } else {
          this.$Message.warning(result.message);
        }
      });
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
    handleeditsubmit() {
      let valid = this.validateRoleForm();
      if (valid) {
        if (this.formSubmitModel.mode === "create") {
          this.doCreateRole();
        }
        if (this.formSubmitModel.mode === "edit") {
          //this.dofinished();
          //this.doEditRole();
          this.doEditSubmit();
          //this.doprogressdeviationedit();
        }
      }
    },
    handleResetFormRole() {
      this.$refs["formRole"].resetFields();
    },
    handleResetFormSubmit() {
      this.$refs["formSubmit"].resetFields();
    },
    doCreateRole() {
      createWorkTask(this.formModel.fields).then((res) => {
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
      editWorkTask(this.formModel.fields).then((res) => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadRoleList();
        } else {
          this.$Message.warning(res.data.message);
        }
        this.handleCloseFormWindow();
      });
    },
    doprogressdeviationedit() {
      progressdeviationedit(this.formSubmitModel.fields).then((res) => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadRoleList();
        } else {
          this.$Message.warning(res.data.message);
        }
        this.handleCloseFormWindow();
      });
    },

    doEditSubmit() {
      editWorkTask(this.formSubmitModel.fields).then((res) => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadRoleList();
        } else {
          this.$Message.warning(res.data.message);
        }
        this.handleCloseSubmitFormWindow();
      });
    },
    // dofinished() {
    //   finishWorkTask(this.formSubmitModel.fields).then((res) => {
    //     if (res.data.code === 200) {
    //       this.$Message.success(res.data.message);
    //       this.loadRoleList();
    //     } else {
    //       this.$Message.warning(res.data.message);
    //     }
    //     this.handleCloseSubmitFormWindow();
    //   });
    // },

    //[key: string]: Vue | Element | Vue[] | Element[];
    validateRoleForm() {
      let _valid = true;
      this.$refs["formRole"].validate((valid) => {
        if (!valid) {
          this.$Message.error("请完善表单信息");
          _valid = false;
        } else {
          _valid = true;
        }
      });
      return _valid;
    },
    validateSubmitForm() {
      let _valid = true;
      this.$refs["formRole"].validate((valid) => {
        if (!valid) {
          this.$Message.error("请完善提交信息");
          _valid = false;
        } else {
          _valid = true;
        }
      });
      return _valid;
    },
    doLoadRole(id) {
      loadWorkTask({ id: id }).then((res) => {
        this.formModel.fields = res.data.data;
      });
    },
    doLoadsubmit(code) {
      loadWorkTask({ code: code }).then((res) => {
        this.formSubmitModel.fields = res.data.data;
      });
    },
    // doLoadprogressdeviation(progressdeviation) {
    //   loadWorkTask({ progressdeviation: progressdeviation }).then((res) => {
    //     this.formModel.fields = res.data.data;
    //   });
    // },

    handleDelete(params) {
      this.doDelete(params.row.id);
    },
    doDelete(ids) {
      if (!ids) {
        this.$Message.warning("请选择至少一条数据");
        return;
      }
      deleteWorkTask(ids).then((res) => {
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
        },
      });
    },
    doBatchCommand(command) {
      batchCommand({
        command: command,
        ids: this.selectedRowsId.join(","),
      }).then((res) => {
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
      this.stores.worktask.query.currentPage = page;
      this.loadRoleList();
    },
    handlePageSizeChanged(pageSize) {
      this.stores.worktask.query.pageSize = pageSize;
      this.loadRoleList();
    },
    handleEcutablekeyword() {
      this.formModel.fields.ecuid = this.stores.worktask.sources.ecutableSources.data[
        this.formModel.fields.Ecutableindex
      ].ecuid;
      this.formModel.fields.electronicUnitNumber = this.stores.worktask.sources.ecutableSources.data[
        this.formModel.fields.Ecutableindex
      ].electronicUnitNumber;
      this.formModel.fields.eunumber = this.stores.worktask.sources.ecutableSources.data[
        this.formModel.fields.Ecutableindex
      ].electronicUnitNumber;
      //console.log(this.formModel.fields.ecuid)
    },
    handleLoadEcutableDataSource(keyword) {
      this.stores.worktask.sources.ecutableSources.loading = true;
      let query = { keyword: keyword };
      findEcutableDataSourceByKeyword(query).then((res) => {
        this.stores.worktask.sources.ecutableSources.data = res.data.data;
        this.stores.worktask.sources.ecutableSources.loading = false;
      });
    },
    handleBasetablekeyword() {
      this.formModel.fields.btid = this.stores.worktask.sources.basetableSources.data[
        this.formModel.fields.Basetableindex
      ].btid;
      this.formModel.fields.batchNumber = this.stores.worktask.sources.basetableSources.data[
        this.formModel.fields.Basetableindex
      ].batchNumber;
      //console.log(this.formModel.fields.ecuid)
    },
    handleLoadBasetableDataSource(keyword) {
      this.stores.worktask.sources.basetableSources.loading = true;
      let query = { keyword: keyword };
      findbasetableDataSourceByKeyword(query).then((res) => {
        this.stores.worktask.sources.basetableSources.data = res.data.data;
        this.stores.worktask.sources.basetableSources.loading = false;
      });
    },
  },
  mounted() {
    this.loadRoleList();
  },
};
</script>
