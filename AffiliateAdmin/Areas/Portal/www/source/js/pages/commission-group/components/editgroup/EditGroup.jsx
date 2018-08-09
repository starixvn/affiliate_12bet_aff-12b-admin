import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import PopupContent from '../shared/PopupContent';
import { defaultPagingOption } from '../../modules/constant';
import { fetchGroupSettingData, getGroupSettingList } from '../../actions';

export class EditGroup extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            selectedGroup: null
        }
    }

    componentDidMount() {
        this._getSelectedGroup();
    }

    _getSelectedGroup = async () => {
        const { itemId } = this.props;
        const response = await fetchGroupSettingData({ GroupId: itemId });
        const data = response.data;
        if (data.CommissionSettingList.length) {
            this.setState({ selectedGroup: data.CommissionSettingList[0] })
        }
        else {
            this.props.onClose();
        }
    }

    handleSave = () => {
        window.setTimeout(this.props.onClose, 3000);
        const gettingParam = Object.assign({}, defaultPagingOption);
        this.props.getGroupSettingList(gettingParam);
    }

    render() {
        const { deductionList, productList } = this.props;
        const { selectedGroup } = this.state;

        return selectedGroup && (
            <PopupContent deductionList={deductionList}
                onSave={() => { }}
                onSuccess={this.handleSave}
                commGroupItem={selectedGroup}
                productList={productList} />
        )
    }
}

function mapStateToProp({ deductionList, productList }) {
    return { deductionList, productList };
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getGroupSettingList }, dispatch);
}

export default connect(mapStateToProp, mapDispatchToProps)(EditGroup);