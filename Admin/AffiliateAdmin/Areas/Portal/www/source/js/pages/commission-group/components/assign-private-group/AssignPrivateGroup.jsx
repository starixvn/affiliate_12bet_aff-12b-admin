import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Alert } from 'reactstrap';

import { getSelectedCommGroup, setSelectedGroup } from '../../actions';
import { buildDefaultCommGroup, validateQmTurnover, validateTierSettingList, validatePlatformFeeList } from '../../modules/service';
import { messages } from '../../modules/constant';

import DeductionSetting from '../shared/deduction';
import ExchangeRevenueShareSetting from '../shared/exchange';
import QmTurnOverSetting from '../shared/QmTurnOverSetting';
import TierSetting from '../shared/tiersetting';
import Loader from '../../../../components/Loader';
import { post } from '../../../../modules/ajaxRequest';
import { popupEventNames } from '../../../common/modules/constants/commissionGroup';

export class AssignPrivateGroup extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            isShowLoader: false,
            isShowMessage: false,
            isSuccess: true,
            message: []
        }
    }

    componentDidMount() {
        this.setState({ isShowLoader: true });
        const { deductionList, productList, affInfo } = this.props;
        const { GroupId } = affInfo;

        if (GroupId === 0 || GroupId !== affInfo.AffId) {
            const commGroup = productList && deductionList && buildDefaultCommGroup(deductionList, productList);
            this.props.setSelectedGroup(commGroup);
        }
        else {
            this.props.getSelectedCommGroup({ GroupId });
        }
        this.setState({ isShowLoader: false });
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
            }, () => window.setTimeout(this.hideMessage, 3000));
        }
        else {
            this.setState({ isShowLoader: true });
            const { selectedGroup, affInfo } = this.props;

            const platformFeeList = selectedGroup.PlatformFeeList.filter(item => !!item.ProductIds);
            const commGroup = { ...selectedGroup, PlatformFeeList: platformFeeList };
            const param = { commGroup, affInfo };

            const response = await post('Portal/CommGroupAssignment/AssignPrivateGroup', param);
            if (response && response.status === 200) {
                const result = response.data;

                const resultMessage = result.IsSuccessful ?
                    (affInfo.GroupId > 0 ? messages.updateGroupSuccessfully : messages.assignGroupSuccessfully) :
                    result.Message;

                window.scrollTo({ top: 0, behavior: "smooth" });

                this.setState({
                    isShowMessage: true,
                    isSuccess: result.IsSuccessful,
                    message: [resultMessage],
                    isShowLoader: false
                }, () => window.setTimeout(() => {
                    this.hideMessage();
                    result.IsSuccessful && parent.postMessage(popupEventNames.updatePrivateGroupSuccessfully, window.location.origin);
                }, 3000));
            }
        }
    }

    hideMessage = () => {
        this.setState({
            isShowMessage: false
        });
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
        const { isShowMessage, isSuccess, message, isShowLoader } = this.state;
        const { affInfo, selectedGroup } = this.props;

        return affInfo && selectedGroup && (
            <div className="comm-popup-container" style={{ padding: 10 }}>
                <Loader show={isShowLoader} />
                <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                    <div dangerouslySetInnerHTML={{ __html: message.join('<br>') }}></div>
                </Alert>
                <ExchangeRevenueShareSetting />
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