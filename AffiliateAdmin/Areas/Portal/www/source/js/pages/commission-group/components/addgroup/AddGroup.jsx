import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import PopupContent from '../shared/PopupContent';
import { defaultPagingOption } from '../../modules/constant';
import { insertCommissionGroup, getGroupSettingList } from '../../actions';

const emptyCommGroup = {
    GroupId: 0,
    GroupName: '',
    Description: '',
    QmTurnover: 0,
    TierSettingList: [],
    PlatformFeeList: [],
    RemainingFeeList: []
}

export class AddGroup extends React.Component {
    constructor(props) {
        super(props);
        console.log("Add Group");
    }

    handleSave = () => {
        window.setTimeout(this.props.onClose, 3000);
        const gettingParam = Object.assign({}, defaultPagingOption);
        this.props.getGroupSettingList(gettingParam);
    }

    render() {
        const { deductionList, productList } = this.props;
        return (
            <PopupContent deductionList={deductionList}
                onSave={this.props.insertCommissionGroup}
                onSuccess={this.handleSave}
                commGroupItem={emptyCommGroup}
                productList={productList} />
        )
    }
}

function mapStateToProp({ deductionList, productList }) {
    return { deductionList, productList };
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ insertCommissionGroup, getGroupSettingList }, dispatch);
}

export default connect(mapStateToProp, mapDispatchToProps)(AddGroup);