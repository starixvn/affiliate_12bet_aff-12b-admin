import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { toggleLoader } from '../../../../actions/loaderAction';
import ReportTitle from '../../../../components/ReportTitle';
import GridData from './GridData';

import { Button, ModalHeader, Modal, ModalBody, Row, Col } from 'reactstrap';

import ReportExcel from '../libs/reportExcel';
import { post } from '../../../../modules/ajaxRequest';
import { claimResource } from '../../../../modules/claim';
import EditRequest from './EditRequest/EditRequest';
import { NotificationPopup } from '../../../common/components/NotificationPopup';
import { deleteProcessingRequest, fetchData, setSelectedRequestId, getProcessingRequestDetail } from '../actions/index';
import { globalResource } from '../libs/constants';

export class Content extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            Popup: null,
            popupTitle: '',
            confirmMsg: null,
            isConfirmPopup: false
        }
    }

    handleAddNewRequest = () => {
        this.setState({
            Popup: (<EditRequest onClose={this.handleHide} />),
            popupTitle: 'Add Processing Request'
        });
    }

    handleEditRequest = async (selectedRequestId) => {
        await this.props.getProcessingRequestDetail(selectedRequestId);
        this.setState({
            Popup: (<EditRequest onClose={this.handleHide} />),
            popupTitle: 'Edit Processing Request'
        });
    }

    handleViewRequest = async (selectedRequestId) => {
        await this.props.getProcessingRequestDetail(selectedRequestId);
        this.setState({
            Popup: (<EditRequest onClose={this.handleHide} isReadOnly={true} />),
            popupTitle: 'Processing Request Detail'
        });
    }

    handleDeleteRequest = () => {
        this.handleShowNotificationPopup('Are you sure to delete this request?', true);
    }

    _handleCloseNotificationPopup = (confirmResult) => {
        const { selectedRequestId } = this.props;
        this.setState({ confirmMsg: '' });

        if (confirmResult && selectedRequestId) {
            this._deleteRequest(selectedRequestId);
        }

        this.props.setSelectedRequestId(null);
    }

    _deleteRequest = async (selectedRequestId) => {
        const result = await this.props.deleteProcessingRequest(selectedRequestId);

        if (result) {
            if (result.IsSuccessful) {
                this.handleShowNotificationPopup('The processing request has been deleted successfully.');

                const { pagingOption, param } = this.props;
                const gettingParam = Object.assign({}, param, { PageIndex: pagingOption.PageIndex, PageSize: pagingOption.PageSize });
                this.props.fetchData(gettingParam);
            }
            else {
                this.handleShowNotificationPopup(result.Message);
            }
        } else {
            this.handleShowNotificationPopup(globalResource.AnError);
        }
    }

    handleHide = () => {
        this.setState({ Popup: null });
    }

    handleShowNotificationPopup = (confirmMsg, isConfirmPopup = false) => {
        this.setState({ confirmMsg, isConfirmPopup });
    }

    handleExportExcel = async () => {
        this.props.toggleLoader(true);
        const { param, reportTitle } = this.props;

        const url = `Portal/ProcessingRequest/GetProcessingRequestList`;
        const postData = Object.assign({}, param, { IsExportExcel: true });
        const response = await post(url, postData);
        let reportExcel = new ReportExcel({ MainData: {}, TotalItem: 0 }, reportTitle)

        if (response && response.status === 200) {
            reportExcel = new ReportExcel(response.data, reportTitle)
        }
        reportExcel.export();

        this.props.toggleLoader(false);
    }

    _buildReportTitle(reportTitle) {
        const { isDisplayExportExcelBtn } = this.props;
        return (
            <div className="d-flex align-items-center">
                {reportTitle}
                {isDisplayExportExcelBtn &&
                    <button type="button" className="ml-auto btn btn-link btn-excel" onClick={this.handleExportExcel} title="Export Excel">
                        <span className="oi icon-excel"></span>
                    </button>
                }
            </div>
        );
    }

    render() {
        const { reportTitle, isModifiable, isClickSearch, dataSummary } = this.props;
        const { Popup, popupTitle, confirmMsg, isConfirmPopup } = this.state;
        const showPopup = Popup !== null;

        return (
            <div className='data-content'>
                {isClickSearch && dataSummary &&
                    <Col className="my-2">
                        <Row>
                            {isModifiable && <Col className="mt-0 pl-0" sm={1}>
                                <Button outline type="button" onClick={this.handleAddNewRequest} title='Add'>Add <span className='oi oi-plus'></span></Button>
                            </Col>}
                            <Col sm={11} className='text-right pr-0'>
                                <b>
                                    <span>Status count:</span>
                                    <span className='pl-3'>{dataSummary.Draft + ' Draft'}</span>
                                    <span className='text-primary pl-3'>{dataSummary.Submitted + ' Submitted'}</span>
                                    <span className='text-success pl-3'>{dataSummary.Approved + ' Approved'}</span>
                                    <span className='text-danger pl-3'>{dataSummary.Rejected + ' Rejected'}</span>
                                </b>
                            </Col>
                    </Row>
                </Col>

                }
                {reportTitle && <ReportTitle content={this._buildReportTitle(reportTitle)} />}
                <GridData handleView={this.handleViewRequest} handleEdit={this.handleEditRequest} handleDelete={this.handleDeleteRequest} />

                <Modal isOpen={showPopup} toggle={this.handleHide} size="lg" backdrop='static'>
                    <ModalHeader toggle={this.handleHide}><span className="iconpopup"></span>{popupTitle}</ModalHeader>
                    <ModalBody>
                        <div className="modal-body-wrapper">
                            {Popup}
                        </div>
                    </ModalBody>
                </Modal>
                {
                    !!confirmMsg && <NotificationPopup
                        message={confirmMsg}
                        isConfirmPopup={isConfirmPopup}
                        onClose={this._handleCloseNotificationPopup}
                    />
                }
            </div>
        );
    }
}

function mapStateToProps({ reportTitle, param, claimsPrincipal, isClickSearch, dataSummary, selectedRequestId, pagingOption }) {
    const isModifiable = claimsPrincipal.hasWriteClaim(claimResource.ProcRequest);
    const isDisplayExportExcelBtn = claimsPrincipal.hasClaim(claimResource.ExportAffInfoExcel);
    return Object.assign({}, { reportTitle, param, isDisplayExportExcelBtn, isModifiable, isClickSearch, dataSummary, selectedRequestId, pagingOption });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ toggleLoader, deleteProcessingRequest, fetchData, setSelectedRequestId, getProcessingRequestDetail }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);