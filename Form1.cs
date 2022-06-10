using System;
using System.Windows.Forms;

namespace AccessPLC
{
	public partial class Form1 : Form
	{
		// PLC연결을 위한 정의
		ActUtlTypeLib.ActUtlType ActUtilType;

		public Form1()
		{
			InitializeComponent();
		}

		private void btnConnect_Click(object sender, EventArgs e)
		{
			// PLC 연결을 위한 라이브러리 정의
			ActUtilType = new ActUtlTypeLib.ActUtlType();

			// 여기의 Station 번호는 MX Component에서 설정한 번호를 정의 한다.
			ActUtilType.ActLogicalStationNumber = 1;

			// PLC 연결. 리턴값이 "0"이면 성공
			int nRtn = ActUtilType.Open();
			if(nRtn == 0)
			{ 
				lbMessage.Text = "PLC에 접속 성공";

				tmrUpdate.Enabled = true;
				tmrUpdate.Interval = 100;
			}
		}

		private void btnDisconnect_Click(object sender, EventArgs e)
		{
			tmrUpdate.Enabled = false;

			int nRtn = ActUtilType.Close();
			if(nRtn == 0) 
				lbMessage.Text = "PLC 접속 닫기 완료";
		}

		private void btnRandom_Click(object sender, EventArgs e)
		{
			int plcData = 0;

			// 스위치 "M1"의 값을 읽어 0인지 1인지 판단한다.
			ActUtilType.GetDevice("M1", out plcData);
			if(plcData == 0)
			{
				ActUtilType.SetDevice("M1", 1);
				lbMessage.Text = "랜덤 발생 스위치 켜기";
			}
			else
			{ 
				ActUtilType.SetDevice("M1", 0);
				lbMessage.Text = "랜덤 발생 스위치 끄기";
			}
		}

		private void tmrUpdate_Tick(object sender, EventArgs e)
		{
			int readData1 = 0;
			int readData2 = 0;
			int readData3 = 0;

			ActUtilType.GetDevice("W1", out readData1);
			ActUtilType.GetDevice("W2", out readData2);
			ActUtilType.GetDevice("W3", out readData3);

			lbRandom1.Text = readData1.ToString();;
			lbRandom2.Text = readData2.ToString();;
			lbRandom3.Text = readData3.ToString();;
		}
	}
}
