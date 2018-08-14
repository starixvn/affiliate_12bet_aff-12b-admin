import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Alert } from 'reactstrap';

import { insertCommissionGroup, setSelectedGroup } from '../../actions';
import { buildDefaultCommGroup, validate } from '../../modules/service';

import DeductionSetting from '../shared/deduction';
import QmTurnOverSetting from '../shared/QmTurnOverSetting';
import TierSetting from '../shared/tiersetting';

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
        const { deductionList, productList } = this.props;
        const commGroup = buildDefaultCommGroup(deductionList, productList);

        this.props.setSelectedGroup(commGroup);
    }

    componentWillUnmount() {
        this.props.setSelectedGroup(null);
    }

    handleSave = async () => {
        const { selectedGroup } = this.props;
        console.log(selectedGroup);
        // const validationResult = validate(selectedGroup);

        // if (!validationResult.isValid) {
        //     this.setState({
        //         isShowMessage: !validationResult.isValid,
        //         isSuccess: validationResult.isValid,
        //         message: validationResult.messageList
        //     });
        // }
        // else {
        //     const platformFeeList = selectedGroup.PlatformFeeList.filter(item => !!item.ProductIds);

        //     const param = { ...selectedGroup, PlatformFeeList: platformFeeList };
        //     const response = await this.props.insertCommissionGroup(param);
        //     const result = response.data;
        //     if (result.IsSuccessful) {
        //         window.setTimeout(this.props.onClose, 3000);
        //         const gettingParam = Object.assign({}, defaultPagingOption);
        //         this.props.getGroupSettingList(gettingParam);
        //     }

        //     this.setState({
        //         isShowMessage: true,
        //         isSuccess: result.IsSuccessful,
        //         message: [result.IsSuccessful ? 'Add Commission Group Successfully' : result.Message]
        //     });
        // }
    }

    render() {
        const { isShowMessage, isSuccess, message } = this.state;
        console.log(this.props.selectedGroup);

        return this.props.selectedGroup && (
            <div className="comm-popup-container">
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

function mapStateToProp({ deductionList, productList, selectedGroup }) {
    return { deductionList, productList, selectedGroup };
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ insertCommissionGroup, setSelectedGroup }, dispatch);
}

export default connect(mapStateToProp, mapDispatchToProps)(AssignPrivateGroup);