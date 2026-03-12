using System.Media;

namespace CatchButton
{
    public partial class Form1 : Form
    {
        // 게임 점수 관리
        private int currentScore = 0;

        // 놓친 횟수 관리 (20번 놓치면 게임 오버)
        private int missCount = 0;

        // 클릭 수 관리 (클릭 성공 횟수)
        private int clickCount = 0;

        // 게임 오버 상태 플래그
        private bool isGameOver = false;

        public Form1()
        {
            InitializeComponent();
            // 버튼 클릭 이벤트 연결
            btnRunAway.Click += BtnRunAway_Click;
            // 초기 점수 표시
            UpdateScore();
        }

        /// <summary>
        /// 버튼 클릭 성공 시 점수 증가 및 난이도 상향
        /// 100점 추가, 버튼 크기 10% 감소, 글자 크기도 함께 감소
        /// 클릭 수 증가
        /// </summary>
        private void BtnRunAway_Click(object sender, EventArgs e)
        {
            // 게임 오버 상태에서는 동작하지 않음
            if (isGameOver)
                return;

            // 100점 추가
            currentScore += 100;

            // 클릭 수 증가
            clickCount++;

            // 성공 효과음 재생
            PlaySuccessSound();

            // 버튼 크기를 10% 작게 조정 (난이도 상향)
            btnRunAway.Width = (int)(btnRunAway.Width * 0.9);
            btnRunAway.Height = (int)(btnRunAway.Height * 0.9);

            // 버튼의 글자 크기도 10% 축소하여 버튼 크기에 알맞게 조정
            btnRunAway.Font = new Font(btnRunAway.Font.FontFamily, btnRunAway.Font.Size * 0.9f);

            // 점수 업데이트
            UpdateScore();
        }

        /// <summary>
        /// 버튼에 마우스가 접근하면 랜덤 위치로 이동
        /// 점수 감점, 놓친 횟수 증가
        /// 놓친 횟수가 20번 이상이면 게임 오버
        /// </summary>
        private void btnRunAway_MouseEnter(object sender, EventArgs e)
        {
            // 게임 오버 상태에서는 동작하지 않음
            if (isGameOver)
                return;

            // 1. 난수 생성기 준비
            Random rd = new Random();

            // 2. 가용영역 계산 (버튼이 폼 테두리에 걸리지 않도록 보호)
            // Location은 버튼의 왼쪽 위 모서리 좌표이므로, 버튼의 크기를 빼야 전체가 폼 안에 들어옴
            int maxX = this.ClientSize.Width - btnRunAway.Width;
            int maxY = this.ClientSize.Height - btnRunAway.Height;

            // 3. 음수 좌표 방지
            maxX = Math.Max(0, maxX);
            maxY = Math.Max(0, maxY);

            // 4. 랜덤 좌표 추출
            int nextX = rd.Next(0, maxX + 1);
            int nextY = rd.Next(0, maxY + 1);

            // 5. 버튼의 새로운 위치 할당
            btnRunAway.Location = new Point(nextX, nextY);

            // 버튼 도망갈 때 점프 효과음 재생
            PlayJumpSound();

            // 10점 감점 (버튼이 도망갈 때마다)
            currentScore -= 10;

            // 점수가 음수가 되지 않도록 0 이상 유지
            if (currentScore < 0)
                currentScore = 0;

            // 놓친 횟수 증가
            missCount++;

            // 점수 업데이트
            UpdateScore();

            // 놓친 횟수가 20번 이상이면 게임 오버 처리
            if (missCount >= 20)
            {
                EndGame();
            }
        }

        /// <summary>
        /// 점수 변화를 폼 제목에 표시
        /// "점수: XXX | 클릭: X | 놓침: X/20" 형식으로 표시
        /// </summary>
        private void UpdateScore()
        {
            this.Text = $"점수: {currentScore} | 클릭: {clickCount} | 놓침: {missCount}/20";
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

        /// <summary>
        /// 게임 오버 상태로 진입
        /// 버튼을 비활성화하고 Game Over 메시지 표시
        /// </summary>
        private void EndGame()
        {
            // 게임 오버 상태 플래그 설정
            isGameOver = true;

            // 버튼 비활성화 (더 이상 클릭 및 마우스 이벤트 처리 안 함)
            btnRunAway.Enabled = false;

            // 게임 오버 메시지 표시 및 다시 시작 옵션 제공
            // MessageBoxButtons.RetryCancel: 재시도(다시 시작) 및 취소 버튼
            DialogResult result = MessageBox.Show(
                $"게임 오버!\n\n최종 점수: {currentScore}\n클릭 수: {clickCount}\n놓친 횟수: {missCount}",
                "Game Over",
                MessageBoxButtons.RetryCancel,
                MessageBoxIcon.Information
            );

            // 다시 시작 버튼 클릭 시
            if (result == DialogResult.Retry)
            {
                RestartGame();
            }
        }

        /// <summary>
        /// 게임 정보 초기화 및 게임 재시작
        /// 점수, 클릭 수, 놓친 횟수, 버튼 크기, 글자 크기 초기화
        /// </summary>
        private void RestartGame()
        {
            // 게임 점수 초기화
            currentScore = 0;

            // 클릭 수 초기화
            clickCount = 0;

            // 놓친 횟수 초기화
            missCount = 0;

            // 게임 오버 상태 해제
            isGameOver = false;

            // 버튼 활성화
            btnRunAway.Enabled = true;

            // 버튼 크기 초기화 (원래 크기: 391 x 107)
            btnRunAway.Width = 391;
            btnRunAway.Height = 107;

            // 버튼 글자 크기 초기화 (원래 크기: 36F)
            btnRunAway.Font = new Font("맑은 고딕", 36F, FontStyle.Regular, GraphicsUnit.Point, 129);

            // 버튼 위치 초기화 (중앙 위치)
            btnRunAway.Location = new Point(248, 199);

            // 점수 표시 업데이트
            UpdateScore();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
