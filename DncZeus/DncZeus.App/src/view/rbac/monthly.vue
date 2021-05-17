
<template>
  <div class="form-style">
    <h1>工作任务录入</h1>
    <hr />

    <Form
      :model="formModel.fields"
      ref="formRole"
      :rules="formModel.rules"
      label-position="left"
    >
      <Row :gutter="32">
        <Col span="30">
          <FormItem label="任务主题" label-position="top">
            <Input
              type="textarea"
              v-model="formModel.fields.taskTheme"
              :rows="2"
              placeholder="请输入任务主题"
            />
          </FormItem>
        </Col>
      </Row>
      <Row :gutter="32">
        <Col span="30">
          <FormItem label="任务内容" label-position="top">
            <Input
              type="textarea"
              v-model="formModel.fields.taskContent"
              :rows="3"
              placeholder="请输入任务内容"
            />
          </FormItem>
        </Col>
      </Row>
      <Row :gutter="32">
        <Col span="8">
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
        <Col span="8">
          <FormItem label="开始时间">
            <DatePicker
              v-model="formModel.fields.completionFirstTime"
              format="yyyy-MM-dd"
              type="date"
              confirm
              placement="bottom-end"
              placeholder="清选择开始时间"
              @on-change="getnowTime"
            ></DatePicker>
          </FormItem>
        </Col>
        <Col span="8">
          <FormItem label="结束时间">
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
          :rows="4"
          placeholder="请输入第三方配合内容"
        />
      </FormItem>
      <FormItem label="注意事项" label-position="top">
        <Input
          type="textarea"
          v-model="formModel.fields.mattersNeedingAttention"
          :rows="4"
          placeholder="请输入注意事项"
        />
      </FormItem>
    </Form>
    <div class="demo-drawer-footer">
      <Button
        icon="md-checkmark-circle"
        type="primary"
        @click="handleSubmitRole"
        >保 存</Button
      >
      <Button style="margin-left: 8px" icon="md-close" @click="deleteallcontent"
        >取 消</Button
      >
    </div>
  </div>
</template>
<script>
import {
  createWorkTask,
} from "@/api/rbac/worktask";
export default {
  name: "rbac_monthly_page",
  data() {
    return {
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
      formItem: {
        input: "",
        select: "",
        radio: "male",
        checkbox: [],
        switch: true,
        date: "",
        time: "",
        slider: [20, 50],
        textarea: "",
      },
      formModel: {
        opened: false,
        title: "创建",
        mode: "create",
        selection: [],
        selectOption: {
          ecutable: {},
        },
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
          //第三方配合事项
          thirdPartyCooperation: "",
          //注意事项
          mattersNeedingAttention: "",
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
              min: 2,
            },
          ],
        },
      },
    };
  },
  methods: {
    handleOpenFormWindow() {
      this.formModel.opened = true;
    },
    handleSubmitRole() {
      let valid = this.validateRoleForm();
      if (valid) {
        if (this.formModel.mode === "create") {
          this.doCreateRole();
          this.deleteallcontent();
        }
        if (this.formModel.mode === "edit") {
          this.doEditRole();
        }
      }
    },

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
    deleteallcontent(){

          //主题
          this.formModel.fields.taskTheme=""
          //任务内容
          this.formModel.fields.taskContent=""
          //任务类型
          this.formModel.fields.workType=""
          //完成时间节点
          this.formModel.fields.completionFirstTime=""
          //完成时间节点-开始
          this.formModel.fields.completionEndTime=""
          //完成时间节点-结束
          this.formModel.fields.completionTime=""
          //任务人
          this.formModel.fields.taskPerson=""
          //联系电话
          this.formModel.fields.telephone=""
          this.formModel.fields.taskplan=""
          this.formModel.fields.planlist=""
          this.formModel.fields.informationCode=""
          //项目经理
          this.formModel.fields.projectManager=""
          //发布人
          this.formModel.fields.publisher=""
          //第三方配合事项
          this.formModel.fields.thirdPartyCooperation=""
          //注意事项
          this.formModel.fields.mattersNeedingAttention=""
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
    handleShowCreateWindow() {
      this.handleSwitchFormModeToCreate();
      this.handleOpenFormWindow();
    },
  },
};
</script>
<style lang="less">
.form-style {
  font-size: 35px;
  margin: 0 auto;
  width: 800px;
  border: 1px solid #f2f2f2;
  border-radius: 5px;
  background-color: #f2f2f2;
  padding: 20px;
  box-shadow: 10px 10px 5px #888888;
}
.dev-style {
  box-shadow: 10px 10px 5px #888888;
}
h1 {
  text-shadow: 5px 5px 5px #f2f2f2;
  font-size: 35px;
}
.h {
  border-bottom: 1px solid black;
}
</style>


