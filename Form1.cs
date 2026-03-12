using System.Media;

namespace CatchButton
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnRunAway_MouseEnter(object sender, EventArgs e)
        {
            // 1. 난수 생성기 준비
            Random rd= new Random();

            // 2. 가용영역 계산 (버튼이 폼 테두리에 걸리지 않도록 보호)
            // Location은 버튼의 왼쪽 위 모서리 좌표이므로, 버튼의 크기를 빼야 전체가 폼 안에 들어옴
            int maxX= this.ClientSize.Width - btnRunAway.Width;
            int maxY= this.ClientSize.Height - btnRunAway.Height;

            // 3. 음수 좌표 방지
            maxX= Math.Max(0, maxX);
            maxY= Math.Max(0, maxY);

            // 4. 랜덤 좌표 추출
            int nextX= rd.Next(0, maxX + 1);
            int nextY= rd.Next(0, maxY + 1);

            // 5. 버튼의 새로운 위치 할당
            btnRunAway.Location= new Point(nextX, nextY);

            // 💫 버튼 도망갈 때 점프 효과음 재생
            PlayJumpSound();

            // 6. 마우스 위치 확인 및 버튼 "잡힘" 감지
            Point mousePositionInForm= this.PointToClient(Cursor.Position);

            // 버튼의 위치와 크기를 포함한 사각형 영역 내에 마우스가 있는지 확인
            if (btnRunAway.Bounds.Contains(mousePositionInForm))
            {
                // 🎉 성공 효과음 재생
                PlaySuccessSound();
                MessageBox.Show("축하합니다~!", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // 7. 시각적 피드백 (폼 제목 표시줄에 현재 버튼 좌표 출력)
            this.Text= $"버튼위치: ({nextX}, {nextY})";
        }

        /// <summary>
        /// 점프 효과음을 재생하는 메서드
        /// 버튼이 마우스를 피해서 도망갈 때 호출됨
        /// Windows 시스템 사운드 사용 (파일 불필요)
        /// </summary>
        private void PlayJumpSound()
        {
            // SystemSounds.Asterisk: 별 모양 경고음 (점프 효과음 대체)
            SystemSounds.Asterisk.Play();
        }

        /// <summary>
        /// 성공 효과음을 재생하는 메서드
        /// 버튼을 성공적으로 잡았을 때 호출됨
        /// Windows 시스템 사운드 사용 (파일 불필요)
        /// </summary>
        private void PlaySuccessSound()
        {
            // SystemSounds.Exclamation: 느낌표 소리 (성공 효과음)
            SystemSounds.Exclamation.Play();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
