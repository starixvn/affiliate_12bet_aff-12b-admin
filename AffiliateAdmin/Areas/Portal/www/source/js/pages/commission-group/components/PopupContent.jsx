import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Alert } from 'reactstrap';

import DeductionSetting from './DeductionSetting';
import QmTurnOverSetting from './QmTurnOverSetting';
import TierSetting from './TierSetting';
import CommGroupInfo from './CommGroupInfo';
import { insertCommissionGroup, setCommGroupAfterInserting, getGroupSettingList, setSelectedGroup } from '../actions';
import { INFINITY_AM, INFINITY_QM, defaultPagingOption, messages } from '../modules/constant';

export class PopupContent extends Component {
    constructor(props) {
        super(props);

        this.commGroupInfo = {};
        this.deductionSettings = {};
        this.qmTurnover = 0;
        this.tierSetting = [];

        this.state = {
            isShowMessage: false,
            isSuccess: true,
            message: []
        }
    }

    handleChangeInput = (name, value) => {
        this.commGroupInfo[name] = value;

    }

    handleSelectDeduction = (deductionSettingList) => {
        this.deductionSettings = deductionSettingList;
    }

    handleChangeInputQmTurnover = (value) => {
        this.qmTurnover = value;
    }

    handleChangeInputTier = (tierSettingList) => {
        this.tierSetting = tierSettingList;
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

        if (this.tierSetting.length === 0) {
            isValid = false;
            messageList.push(messages.requireTier);
        }
        else if (this.tierSetting.filter(item => item.infinityAm && item.infinityQm).length === 0) {
            isValid = false;
            messageList.push(messages.requireInfinity);
        }
        else if(this.tierSetting.filter(item => item.minAmount > item.maxAmount).length > 0){
            isValid = false;
            messageList.push(messages.amountConstrain);
        }
        else if(this.tierSetting.filter(item => item.minQm > item.maxQm).length > 0){
            isValid = false;
            messageList.push(messages.qmConstrain);
        }

        return { isValid, messageList };
    }

    handleSave = async () => {
        const validationResult = this._isValid();

        if (!validationResult.isValid) {
            this.setState({
                isShowMessage: !validationResult.isValid,
                isSuccess: validationResult.isValid,
                message: validationResult.messageList
            });
        }
        else {
            const tierSettingList = this.tierSetting.map(item => {
                return {
                    tierId: item.tierId,
                    minAmount: item.minAmount,
                    maxAmount: item.infinityAm ? INFINITY_AM : item.maxAmount,
                    minQm: item.minQm,
                    maxQm: item.infinityQm ? INFINITY_QM : item.maxQm,
                    commission: item.commission
                }
            })

            const param = Object.assign({},
                this.commGroupInfo,
                this.deductionSettings,
                { qmTurnover: this.qmTurnover },
                { tierSettingList });

            console.log(param);
            const response = await this.props.insertCommissionGroup(param);
            const result = response.data;
            console.log(result);
            if (result.IsSuccessful) {
                this.props.setSelectedGroup(null);
                const gettingParam = Object.assign({}, defaultPagingOption);
                this.props.getGroupSettingList(gettingParam);
            }
        }
    }

    render() {
        const { isShowMessage, isSuccess, message } = this.state;
        return (
            <div className="comm-popup-container">
                <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                    <div dangerouslySetInnerHTML={{ __html: message.join('<br>') }}></div>
                </Alert>
                <CommGroupInfo onChange={this.handleChangeInput} />
                <DeductionSetting onSelect={this.handleSelectDeduction} />
                <QmTurnOverSetting onChange={this.handleChangeInputQmTurnover} />
                <TierSetting onChange={this.handleChangeInputTier} />

                <button type="button" className="my-2" onClick={this.handleSave}>SAVE</button>
            </div>
        )
    }
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ insertCommissionGroup, setCommGroupAfterInserting, getGroupSettingList, setSelectedGroup }, dispatch);
}

export default connect(null, mapDispatchToProps)(PopupContent);