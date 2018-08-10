import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Alert } from 'reactstrap';

import { insertCommissionGroup, getGroupSettingList, setSelectedGroup } from '../../actions';
import { buildDefaultCommGroup } from '../../modules/service';

import DeductionSetting from '../shared/deduction';
import QmTurnOverSetting from '../shared/QmTurnOverSetting';
import TierSetting from '../shared/tiersetting';
import CommGroupInfo from '../shared/CommGroupInfo';
import { INFINITY_AM, INFINITY_QM, defaultPagingOption, messages } from '../../modules/constant';

export class AddGroup extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            isShowMessage: false,
            isSuccess: true,
            message: []
        }

        const { deductionList, productList } = this.props;
        const commGroup = buildDefaultCommGroup(deductionList, productList);

        this.props.setSelectedGroup(commGroup);
    }

    _isValid() {
        let isValid = true;
        const messageList = [];
        const { selectedGroup } = this.props;

        if (!selectedGroup.GroupName) {
            isValid = false;
            messageList.push(messages.requireGroupName);
        }

        if (isNaN(selectedGroup.QmTurnover) || typeof selectedGroup.QmTurnover !== 'number') {
            isValid = false;
            messageList.push(messages.requireQmTurnover);
        }

        if (selectedGroup.TierSettingList.length === 0) {
            isValid = false;
            messageList.push(messages.requireTier);
        }
        else if (selectedGroup.TierSettingList.filter(item => item.MaxAmount === INFINITY_AM && item.MaxQm === INFINITY_QM).length === 0) {
            isValid = false;
            messageList.push(messages.requireInfinity);
        }
        else if (selectedGroup.TierSettingList.filter(item => item.MinAmount > item.MaxAmount).length > 0) {
            isValid = false;
            messageList.push(messages.amountConstrain);
        }
        else if (selectedGroup.TierSettingList.filter(item => item.MinQm > item.MaxQm).length > 0) {
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
            const { selectedGroup } = this.props;

            const platformFeeList = selectedGroup.PlatformFeeList.filter(item => !!item.ProductIds);

            const param = { ...selectedGroup, PlatformFeeList: platformFeeList };
            console.log(param);

            const response = await this.props.insertCommissionGroup(param);
            const result = response.data;
            console.log(result);
            if (result.IsSuccessful) {
                window.setTimeout(this.props.onClose, 3000);
                const gettingParam = Object.assign({}, defaultPagingOption);
                this.props.getGroupSettingList(gettingParam);
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

        return (
            <div className="comm-popup-container">
                <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                    <div dangerouslySetInnerHTML={{ __html: message.join('<br>') }}></div>
                </Alert>
                <CommGroupInfo />
                <DeductionSetting />
                <QmTurnOverSetting />
                <TierSetting />

                <button type="button" className="my-2 btn btn-success btn-save" onClick={this.handleSave}>SAVE</button>
            </div>
        )
    }
}

function mapStateToProp({ deductionList, productList, selectedGroup }) {
    return { deductionList, productList, selectedGroup };
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ insertCommissionGroup, getGroupSettingList, setSelectedGroup }, dispatch);
}

export default connect(mapStateToProp, mapDispatchToProps)(AddGroup);