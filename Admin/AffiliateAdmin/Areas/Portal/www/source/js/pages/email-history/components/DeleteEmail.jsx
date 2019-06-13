import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button } from 'reactstrap';

import { resource, messages } from '../modules/constants';
import { fetchData, deleteEmailHistory, toggleLoader } from '../actions';

export class DeleteEmail extends PureComponent {
    constructor(props) {
        super(props);
    }

    handleDelete = async () => {
        const emailIdList = this._getCheckedEmailIdList();

        if (emailIdList.length === 0) {
            alert(messages.PleaseSelect);
            return;
        }

        const isConfirmed = window.confirm(messages.AreYouSureDeleteEmails);
        if (isConfirmed) {
            this.props.toggleLoader(true);

            const param = {
                EmailIdList: emailIdList.join(',')
            };

            const result = await this.props.deleteEmailHistory(param);

            if (result) {
                if (result.IsSuccessful) {
                    alert(messages.DeleteEmails);

                    this.props.fetchData(this.props.param);
                }
                else {
                    alert(result.Message);
                }
            }

            this.props.toggleLoader(false);
        }
    }

    _getCheckedEmailIdList() {
        const { checkedEmails } = this.props;

        return Object.keys(checkedEmails).reduce((accumulator, currentValue) => {
            if (checkedEmails[currentValue]) {
                accumulator.push(currentValue);
            }

            return accumulator;
        }, []);
    }

    render() {
        return (
            <div className="my-2">
                <Button outline type="button" color="danger" className="mr-2" onClick={this.handleDelete}>{resource.Delete}<span className="oi oi-x ml-2"></span></Button>
            </div>
        );
    }
}


function mapStateToProps({ checkedEmails, param }) {
    return Object.assign({}, { checkedEmails, param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchData, deleteEmailHistory, toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(DeleteEmail);