import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { validateFileExtension } from '../libs/utils';
import { setIsClickUpload, toggleLoader, handleUploadFile } from '../actions';

export class UploadResult extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            result: {
                isOk: true,
                message: ''
            },
            responseData: null,
        }
    }

    componentDidMount() {
        this.handleUploadFile();
    }

    componentDidUpdate(prevProps) {
        const { isClickUpload } = this.props;

        if (isClickUpload && isClickUpload !== prevProps.isClickUpload) {
            this.handleUploadFile();
        }
    }

    validateFile = (file) => {
        return validateFileExtension(file.name);
    }

    handleUploadFile = async () => {
        this.props.toggleLoader(true);

        const { file } = this.props;

        let responseData = null;
        let validateFileResult = this.validateFile(file);

        if (validateFileResult.isOK) {
            let resultInfo = await this.props.handleUploadFile(file).then(re => re);

            if (resultInfo.Result.IsSuccessful) {
                validateFileResult.message = 'Your file has been upload successfully!';
            } else {
                validateFileResult = {
                    isOK: false,
                    message: resultInfo.Result.Message
                };

                responseData = resultInfo.ErroredAdjustmentInfoList;
            }
        }

        this.setState({ result: validateFileResult, responseData });

        this.props.setIsClickUpload(false);
        this.props.toggleLoader(false);
    }

    renderRow = () => {
        const rows = [];
        const { responseData } = this.state;

        for (let i = 0; i < responseData.length; i++) {
            const item = responseData[i];
            const index = i + 1;

            rows.push(
                <tr key={i}>
                    <td>{index}</td>
                    <td colSpan="3">{item.Username}</td>
                    <td>{item.Amount}</td>
                    <td>{item.Reason}</td>
                </tr>
            )
        }

        return rows;
    };

    handleResponseData = () => {
        return (
            <table className="data-table mt-3">
                <thead>
                    <th>Stt</th>
                    <th colSpan="3">User Name</th>
                    <th>Amount</th>
                    <th>Error</th>
                </thead>
                <tbody>
                    {
                        this.renderRow()
                    }
                </tbody>
            </table>
        )
    };

    render() {
        const { result, responseData } = this.state;

        return (
            <div className="result-container">
                {
                    result.isOK ? (
                        <div className="success-alert">
                            <span className="text-success">{result.message}</span>
                        </div>
                    ) : (
                            <div className="error-alert">
                                <div className="text-danger">
                                    <p><strong>Error:</strong></p>
                                    <div className="my-2" dangerouslySetInnerHTML={{ __html: result.message }}></div>
                                </div >
                                <p>Please check and re-upload the file.</p>
                                {
                                    responseData && !!responseData.length && (
                                        this.handleResponseData()
                                    )
                                }
                            </div >
                        )
                }
            </div>
        );
    }
}

function mapStateToProps({ file, isClickUpload }) {
    return Object.assign({}, { file, isClickUpload });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setIsClickUpload, toggleLoader, handleUploadFile }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(UploadResult);