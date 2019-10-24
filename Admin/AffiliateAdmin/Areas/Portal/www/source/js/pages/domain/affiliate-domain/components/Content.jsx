import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';
import { NotificationPopup } from '../../../common/components/NotificationPopup';

import { htmlDecode } from '../../../../modules/htmlHelper';
import { headerText, globalResource } from '../../common/libs/constants';
import { deleteDomain, fetchData, toggleLoader, setShouldResetScrollPosition } from '../actions';

export class Content extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            selectedDomainId: 0,
            message: '',
            isConfirmPopup: true
        }
    }

    componentDidMount() {
        this.props.fetchData();
    }

    _handleCloseNotificationPopup = (confirmResult) => {
        this.setState({ message: '' });

        const { selectedDomainId } = this.state;
        if (confirmResult && selectedDomainId) {
            this._deleteDomain(selectedDomainId);
        }
    }

    _handleDeleteDomain = (selectedDomainId) => {
        this.props.setShouldResetScrollPosition(false);
        this.setState({ message: 'Are you sure you want to delete this affiliate domain?', isConfirmPopup: true, selectedDomainId });
    }

    _handleEditDomain = (selectedDomainId) => {
        this.props.setShouldResetScrollPosition(false);
        this.selectedDomainId(selectedDomainId);
    }

    _deleteDomain = async (selectedDomainId) => {
        this.props.toggleLoader(true);

        const result = await this.props.deleteDomain(selectedDomainId);

        if (result) {
            if (result.IsSuccessful) {
                this.setState({ message: 'Affiliate domain has been removed suseccfully!', isConfirmPopup: false });

                this.props.fetchData(this.props.param);
            }
            else {
                this.setState({ message: result.Message, isConfirmPopup: false });
            }
        }

        this.props.toggleLoader(false);
    }

    _renderHeader() {
        return (
            <Row key="thead">
                <Cell header={true} colWidth={20} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={150} title={headerText.Domain}>{headerText.Domain}</Cell>
                <Cell header={true} colWidth={20} title={headerText.SSL}>{headerText.SSL}</Cell>
                <Cell header={true} colWidth={40} title={headerText.Creator}>{headerText.Creator}</Cell>
                <Cell header={true} colWidth={60} title={headerText.CreatedDate}>{headerText.CreatedDate}</Cell>
                <Cell header={true} colWidth={20} title={headerText.Edit}>{headerText.Edit}</Cell>
                <Cell header={true} colWidth={20} title={headerText.Delete}>{headerText.Delete}</Cell>
            </Row>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList && dataList.length) {
            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(this._buildDataRow(dataItem, i + 1));
            }
        }
        else {
            dataRows.push(<Row>
                <Cell className="text-center" colSpan="7">{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    _buildDataRow(dataItem, index) {
        const domain = htmlDecode(dataItem.Domain);
        const creator = htmlDecode(dataItem.Creator);

        return (
            <Row key={`dataRow-${index}`}>
                <Cell className="text-center" title={index}>{index}</Cell>
                <Cell className="text-center" title={domain}>{domain}</Cell>
                <Cell className="text-center">
                    <input type="checkbox" checked={dataItem.IsSSL} />
                </Cell>
                <Cell className="text-center" title={creator}>{creator}</Cell>
                <Cell className="text-center" title={dataItem.CreatedDateText}>{dataItem.CreatedDateText}</Cell>
                <Cell className="text-center" title="Edit Affiliate Domain">
                    <Button color="link p-0" onClick={() => this._handleEditDomain(dataItem.DomainId)}>
                        <span className="oi oi-pencil text-secondary"></span>
                    </Button>
                </Cell>
                <Cell className="text-center" title="Delete Affiliate Domain">
                    <Button color="link p-0" onClick={() => this._handleDeleteDomain(dataItem.DomainId)}>
                        <span className="oi oi-x text-danger"></span>
                    </Button>
                </Cell>
            </Row>
        )
    }

    render() {
        const { gridData, shouldResetScrollPosition } = this.props;
        const { message, isConfirmPopup } = this.state;

        return gridData && (
            <div className="data-content">
                <div className="my-2">
                    <Button outline type="button" onClick={() => this.handleShowPopup(0)}>Add AffDomain <span className="oi oi-plus ml-2"></span></Button>
                </div>
                <div className="Domain-list-content table-responsive table-striped my-2">
                    <Table
                        minWidth={1000}
                        autoWidth={true}
                        bodyHeight={300}
                        header={this._renderHeader()}
                        body={this._renderDataSection(gridData)}
                        containerPadding={20}
                        onPaging={this._handlePaging}
                        shouldResetScrollPosition={shouldResetScrollPosition}
                    />
                </div>
                {
                    !!message && <NotificationPopup
                        message={message}
                        isConfirmPopup={isConfirmPopup}
                        onClose={this._handleCloseNotificationPopup}
                    />
                }
            </div>
        );
    }
}

function mapStateToProps({ shouldResetScrollPosition, gridData }) {
    return Object.assign({}, { shouldResetScrollPosition, gridData });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ deleteDomain, fetchData, toggleLoader, selectedDomainId, setShouldResetScrollPosition }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);