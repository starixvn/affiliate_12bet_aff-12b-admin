import React, { Component } from 'react';
import { Alert } from 'reactstrap';

import DeductionSetting from './deduction';
import QmTurnOverSetting from './QmTurnOverSetting';
import TierSetting from './tiersetting';
import CommGroupInfo from './CommGroupInfo';
import { INFINITY_AM, INFINITY_QM, messages } from '../../modules/constant';

export default class PopupContent extends Component {
    constructor(props) {
        super(props);

        const dataItem = this.props.commGroupItem;

        this.deductionSettings = {
            PlatformFeeList: dataItem.PlatformFeeList,
            RemainingFeeList: dataItem.RemainingFeeList
        };
        this.qmTurnover = dataItem.QmTurnover;
        this.tierSettingList = dataItem.TierSettingList;

        this.state = {
            isShowMessage: false,
            isSuccess: true,
            message: []
        }

        this.groupInfoRef = React.createRef();
        this.deductionRef = React.createRef();
        this.qmTurnoverRef = React.createRef();
        this.tierSettingRef = React.createRef();
    }

    handleSelectDeduction = (deductionSettingList) => {
        this.deductionSettings = deductionSettingList;
    }

    handleChangeInputQmTurnover = (value) => {
        this.qmTurnover = value;
    }

    handleChangeInputTier = (tierSettingList) => {
        this.tierSettingList = tierSettingList;
    }

    _isValid() {
        let isValid = true;
        const messageList = [];

        if (!this.commGroupInfo.GroupName) {
            isValid = false;
            messageList.push(messages.requireGroupName);
        }

        if (isNaN(this.qmTurnover) || typeof this.qmTurnover !== 'number') {
            isValid = false;
            messageList.push(messages.requireQmTurnover);
        }

        if (this.tierSettingList.length === 0) {
            isValid = false;
            messageList.push(messages.requireTier);
        }
        else if (this.tierSettingList.filter(item => item.InfinityAm && item.InfinityQm).length === 0) {
            isValid = false;
            messageList.push(messages.requireInfinity);
        }
        else if (this.tierSettingList.filter(item => item.MinAmount > item.MaxAmount).length > 0) {
            isValid = false;
            messageList.push(messages.amountConstrain);
        }
        else if (this.tierSettingList.filter(item => item.MinQm > item.MaxQm).length > 0) {
            isValid = false;
            messageList.push(messages.qmConstrain);
        }

        return { isValid, messageList };
    }

    handleSave = async () => {
        const commGroupInfo = this.groupInfoRef.current.getValue();
        const deductions = this.deductionRef.current.getValue();
        console.log(commGroupInfo);
        console.log(deductions);
        return;

        const validationResult = this._isValid();

        if (!validationResult.isValid) {
            this.setState({
                isShowMessage: !validationResult.isValid,
                isSuccess: validationResult.isValid,
                message: validationResult.messageList
            });
        }
        else {
            const tierSettingList = this.tierSettingList.map(item => {
                return {
                    TierId: item.TierId,
                    MinAmount: item.MinAmount,
                    MaxAmount: item.InfinityAm ? INFINITY_AM : item.MaxAmount,
                    MinQm: item.MinQm,
                    MaxQm: item.InfinityQm ? INFINITY_QM : item.MaxQm,
                    Commission: item.Commission
                }
            })

            const param = {
                GroupId: this.commGroupInfo.GroupId,
                GroupName: this.commGroupInfo.GroupName,
                Description: this.commGroupInfo.Description,
                QmTurnover: this.qmTurnover,
                TierSettingList: tierSettingList,
                PlatformFeeList: this.deductionSettings.PlatformFeeList,
                RemainingFeeList: this.deductionSettings.RemainingFeeList
            }

            console.log(param);

            const { onSave, onSuccess } = this.props;
            const response = await onSave(param);
            const result = response.data;
            console.log(result);
            if (result.IsSuccessful) {
                onSuccess();
            }

            this.setState({
                isShowMessage: true,
                isSuccess: result.IsSuccessful,
                message: [result.IsSuccessful ? 'Add Commission Group Successfully' : result.Message]
            });
        }
    }

    render() {       
        const { isShowMessage, isSuccess, message } = this.state;
        const { deductionList, productList, commGroupItem } = this.props;

        const commGroupInfo = {
            GroupId: commGroupItem.GroupId,
            GroupName: commGroupItem.GroupName,
            Description: commGroupItem.Description
        };

        const deductionSettings = {
            PlatformFeeList: commGroupItem.PlatformFeeList,
            RemainingFeeList: commGroupItem.RemainingFeeList
        };

        const tierSettingData = this.tierSettingList.map(item => {
            const { MinAmount, MaxAmount, MinQm, MaxQm, Commission } = item;

            return {
                MinAmount,
                MaxAmount,
                MinQm,
                MaxQm,
                Commission,
                InfinityAm: MaxAmount === INFINITY_AM,
                InfinityQm: MaxQm === INFINITY_QM
            }
        })

        return (
            <div className="comm-popup-container">
                <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                    <div dangerouslySetInnerHTML={{ __html: message.join('<br>') }}></div>
                </Alert>
                <CommGroupInfo ref={this.groupInfoRef} data={commGroupInfo} />
                <DeductionSetting ref={this.deductionRef} onSelect={this.handleSelectDeduction}
                    deductionList={deductionList}
                    productList={productList}
                    data={deductionSettings} />
                <QmTurnOverSetting onChange={this.handleChangeInputQmTurnover} data={this.qmTurnover} />
                <TierSetting onChange={this.handleChangeInputTier} data={tierSettingData} />

                <button type="button" className="my-2 btn btn-success btn-save" onClick={this.handleSave}>SAVE</button>
            </div>
        )
    }
}