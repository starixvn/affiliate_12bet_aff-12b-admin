import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Alert } from 'reactstrap';

import { getSelectedCommGroup, setSelectedGroup } from '../../actions';
import { buildDefaultCommGroup, validateQmTurnover, validateTierSettingList, validatePlatformFeeList } from '../../modules/service';

import DeductionSetting from '../shared/deduction';
import QmTurnOverSetting from '../shared/QmTurnOverSetting';
import TierSetting from '../shared/tiersetting';
import { post } from '../../../../modules/ajaxRequest';

export class AssignPrivateGroup extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            isShowMessage: false,
            isSuccess: true,
            message: []
        }
    }

    componentDidMount() {
        const { deductionList, productList, affInfo } = this.props;
        const { GroupId } = affInfo;

        if (GroupId === 0 || GroupId !== affInfo.AffId) {
            const commGroup = buildDefaultCommGroup(deductionList, productList);
            this.props.setSelectedGroup(commGroup);
        }
        else {
            this.props.getSelectedCommGroup({GroupId});
        }
    }

    componentWillUnmount() {
        this.props.setSelectedGroup(null);
    }

    handleSave = async () => {
        const validationResult = this._validate();

        if (!validationResult.isValid) {
            this.setState({
                isShowMessage: !validationResult.isValid,
                isSuccess: validationResult.isValid,
                message: validationResult.messageList
            });
        }
        else {
            const { selectedGroup, affInfo } = this.props;

            const platformFeeList = selectedGroup.PlatformFeeList.filter(item => !!item.ProductIds);
            const commGroup = { ...selectedGroup, PlatformFeeList: platformFeeList };
            const param = { commGroup, affInfo };

            const response = await post('Portal/CommissionGroup/AssignPrivateGroup', param);
            const result = response.data;

            if (result.IsSuccessful) {
                const mainFrame = top.frames["mainFrame"];
                if (mainFrame) {
                    mainFrame.location.reload();
                }
                else {
                    window.location.reload();
                }
            }
            else {
                window.scrollTo({top: 0, behavior: "smooth"});
                this.setState({
                    isShowMessage: true,
                    isSuccess: false,
                    message: [result.Message]
                });
            }
        }
    }

    _validate() {
        const { QmTurnover, TierSettingList, PlatformFeeList } = this.props.selectedGroup;
        const qmTurnoverValidation = validateQmTurnover(QmTurnover);
        const tierSettingValidation = validateTierSettingList(TierSettingList);
        const platformFeeListValidation = validatePlatformFeeList(PlatformFeeList);

        const isValid = qmTurnoverValidation.isValid && tierSettingValidation.isValid && platformFeeListValidation.isValid;
        const messageList = [...qmTurnoverValidation.messageList, ...tierSettingValidation.messageList,
        ...platformFeeListValidation.messageList
        ];

        return { isValid, messageList };
    }

    render() {
        const { isShowMessage, isSuccess, message } = this.state;

        return this.props.selectedGroup && (
            <div className="comm-popup-container" style={{ padding: 10 }}>
                <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                    <div dangerouslySetInnerHTML={{ __html: message.join('<br>') }}></div>
                </Alert>
                <DeductionSetting />
                <QmTurnOverSetting />
                <TierSetting />

                <button type="button" className="my-2 btn btn-success btn-save" onClick={this.handleSave}>SAVE</button>
            </div>
        )
    }
}

function mapStateToProp({ deductionList, productList, selectedGroup, affInfo }) {
    return { deductionList, productList, selectedGroup, affInfo };
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getSelectedCommGroup, setSelectedGroup }, dispatch);
}

export default connect(mapStateToProp, mapDispatchToProps)(AssignPrivateGroup);