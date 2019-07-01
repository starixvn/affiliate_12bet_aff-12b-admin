import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Alert, Col, Button, Form, FormGroup, Label, Input, Modal, ModalHeader, ModalBody } from 'reactstrap';
import { fetchData, setSelectedEmailId, getEmailHistoryDetail, toggleLoader, deleteEmailHistory } from '../actions';

import { resource, globalResource, messages } from '../modules/constants';

export class EmailDetailForm extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            dataItem: null,
            isShowMessage: false,
            isSuccess: true,
            message: ''
        }
    }

    componentDidUpdate(prevProps) {
        const { selectedEmailId } = this.props;

        if (selectedEmailId && selectedEmailId !== prevProps.selectedEmailId) {
            this.handleGetEmailHistoryDetail(selectedEmailId);
        }
    }

    handleGetEmailHistoryDetail = async (selectedEmailId) => {
        this.props.toggleLoader(true);

        const dataItem = await this.props.getEmailHistoryDetail(selectedEmailId);
        this.setState({ dataItem });

        this.props.toggleLoader(false);
    }

    handleDelete = async (selectedEmailId) => {
        const isConfirmed = window.confirm(messages.AreYouSureDeleteEmail);

        if (isConfirmed) {
            this.props.toggleLoader(true);

            const param = {
                EmailIdList: selectedEmailId
            }

            const result = await this.props.deleteEmailHistory(param);

            if (result) {
                this.setState({
                    isSuccess: result.IsSuccessful,
                    isShowMessage: true,
                    message: result.IsSuccessful ? messages.DeleteEmail : result.Message
                }, () => window.setTimeout(this.hideMessage, 3000));

                result.IsSuccessful && window.setTimeout(() => {
                    this.handleClosePopup();
                    this.props.fetchData(this.props.param);
                }, 3100);
            }

            this.props.toggleLoader(false);
        }
    }

    hideMessage = () => {
        this.setState({ isShowMessage: false });
    }

    handleClosePopup = () => {
        this.props.setSelectedEmailId(null);
    }

    render() {
        const { selectedEmailId } = this.props;
        const { dataItem, isSuccess, isShowMessage, message } = this.state;
        const href = dataItem && `mailto:${dataItem.Email}`;

        return dataItem && (
            <Modal isOpen={!!selectedEmailId} toggle={this.handleClosePopup} backdrop='static'>
                <ModalHeader toggle={this.handleClosePopup}><span className="iconpopup"></span>{resource.EmailDetail}</ModalHeader>
                <ModalBody>
                    <div className="modal-body-wrapper">
                        <div className="note-form-container">
                            <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                                <div dangerouslySetInnerHTML={{ __html: message }}></div>
                            </Alert>
                            <Form tag="form" className="mb-3" autocomplete="off">
                                <FormGroup row>
                                    <Label className="py-1" sm={3}>{resource.Subject}</Label>
                                    <Label className="py-1" sm={9}>{dataItem.Subject}</Label>
                                </FormGroup>
                                <FormGroup row>
                                    <Label className="py-1" sm={3}>{resource.From}</Label>
                                    <Label className="py-1" sm={9}>{dataItem.Sender}<a target="_top" href={href}> ({dataItem.Email})</a></Label>
                                </FormGroup>
                                <FormGroup row>
                                    <Label className="py-1" sm={3}>{resource.SentDate}</Label>
                                    <Label className="py-1" sm={9}>{dataItem.SentDateText}</Label>
                                </FormGroup>
                                <FormGroup row>
                                    <Label className="py-1" sm={3} className="d-flex align-items-center">{resource.Content}</Label>
                                    <Col sm={9}>
                                        <Input type="textarea" rows="8" value={dataItem.Content} readonly="readonly" />
                                    </Col>
                                </FormGroup>
                            </Form>
                            <Button type="button" color="danger" className="mr-3" onClick={() => this.handleDelete(selectedEmailId)}>{resource.Delete}</Button>
                            <Button type="button" color="info" onClick={this.handleClosePopup}>{globalResource.Cancel}</Button>
                        </div>
                    </div>
                </ModalBody>
            </Modal>
        );
    }
}

function mapStateToProps({ selectedEmailId, param }) {

    return Object.assign({}, { selectedEmailId, param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchData, setSelectedEmailId, getEmailHistoryDetail, toggleLoader, deleteEmailHistory }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(EmailDetailForm);